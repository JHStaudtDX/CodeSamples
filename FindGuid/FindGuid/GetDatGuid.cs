using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Relativity.API;

/* MENTAL NOTES 
 * I would like to take the functionality from checking the inputString for 
 * numbers and apply it to checking the workspace input for digits. Then I
 * can actually allow search by workspace name, not just artifactID 
 */
namespace FindGuid
{
    public partial class GetDatGuid : Form
    {

        public static string finalWorkspaceName = "";
        public GetDatGuid()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public void findGUIDButton_Click(object sender, EventArgs e)
        {
            aIDNameBox.Text = "";
            guidBox.Text = "";

            try
            {
                ConnectionManager cmr = new ConnectionManager();
                using (var proxy = cmr.GetRsapiClient())
                {
                    var workspaceString = workspaceBox.Text;
                    var objectString = inputBox.Text;
                    var workspaceList = new string[]
                        {"-1", "Admin", "admin", "EDDS", "edds", "Instance", "instance"}; //
                    
                    var tableInfoArtifactGuidEDDS = "[EDDS].[EDDSDBO].[ArtifactGuid]";
                    var tableInfoArtifactEDDS = "[EDDS].[EDDSDBO].[Artifact]";
                    var tableInfoArtifactGuid = "[EDDS" + workspaceString + "].[EDDSDBO].[ArtifactGuid]";
                    var tableInfoArtifact = "[EDDS" + workspaceString + "].[EDDSDBO].[Artifact]";
                    //proxy.APIOptions.WorkspaceID = Convert.ToInt32(workspaceString);
                    IDBContext contextDB;// = GetDBContext(Convert.ToInt32(workspaceString));
                    #region Weird Ops with workspace name lol
                    bool workspaceNameorArtifactID =  workspaceString.Any(y => !char.IsDigit(y));
                   
                    if (workspaceNameorArtifactID) //then we are working with a name, not an artiID
                    {
                        var sqlParamInitial = new List<SqlParameter>
                        {
                            new SqlParameter("@workspaceString", workspaceString),
                            new SqlParameter("@objectString", objectString)
                        };
                        contextDB = GetDBContext(-1);

                        string sqlWorkspaceName =
                            "SELECT [Name] FROM [EDDS].[EDDSDBO].[Case] WHERE artifactID = @workspaceString";
                       var workspaceNameReader = contextDB.ExecuteParameterizedSQLStatementAsReader(sqlWorkspaceName, sqlParamInitial);
                        var workspaceNameVal = "";
                        while (workspaceNameReader.Read())
                        {
                             workspaceNameVal = workspaceNameReader.GetString(0);
                             //This reads the results of the the SQL query into a string to work with in VS.
                            //aIDNameBox.Text = artifactIDVal.ToString();
                            //guidBox.Text = (guidValue.ToString()); //Sets guidBox to the GUID found.
                            finalWorkspaceName = workspaceNameVal;
                        }
                        workspaceNameReader.Close();
                        workspaceString = workspaceNameVal;
                    }
                    #endregion
                    #region ADMIN LEVEL
                    if (workspaceList.Contains(workspaceString)) 
                    {
                        contextDB = GetDBContext(-1);

                        var sqlParams = new List<SqlParameter>
                        {
                            new SqlParameter("@workspaceString", workspaceString),
                            new SqlParameter("@objectString", objectString)
                        };
                        
                      
                        bool resultObject = objectString.Any(x => !char.IsDigit(x)); //Check for alphabet chars in string //switched from using IsChar to test isDigit - I just want to be able to separate artiID and Name input.

                        if (resultObject) // then we are working with a string
                        {
                            string sqlString = "SELECT b.ArtifactGuid, a.ArtifactID from " + tableInfoArtifactEDDS +
                                               " a INNER JOIN " + tableInfoArtifactGuidEDDS +
                                               " b ON a.ArtifactID = b.ArtifactID WHERE a.TextIdentifier = @objectString";
                            //If inputstring is a string this means that we are working with a name NOT
                            //an ArtifactID
                            var reader =
                                contextDB.ExecuteParameterizedSQLStatementAsReader(sqlString,
                                    sqlParams); //I'll need to figure out the db connection for this to work.
                            while (reader.Read())
                            {
                                var artifactIDVal = reader.GetInt32(1);
                                ;
                                var guidValue =
                                    reader.GetGuid(
                                        0); //This reads the results of the the SQL query into a string to work with in VS.
                                aIDNameBox.Text = artifactIDVal.ToString();
                                guidBox.Text = (guidValue.ToString()); //Sets guidBox to the GUID found.
                            }
                            reader.Close();
                            //Get Object Name
                        }
                        else
                        {
                            var sqlParam = new List<SqlParameter>
                            {
                                new SqlParameter("@workspaceString", workspaceString),
                                new SqlParameter("@objectString", objectString)

                            };


                            //If inputstring is NOT a string this means we are working with an artifactID 
                            //MOST LIKELY. Someone could have named something will all numbers. Refine this
                            //logic at a later date. Also refine the logic for dealing with admin instace

                            string sqlArtifactID = "SELECT [ArtifactGuid] FROM " + tableInfoArtifactGuidEDDS +
                                                   " WHERE [ArtifactID] = @objectString";
                            var reader =
                                contextDB.ExecuteParameterizedSQLStatementAsReader(sqlArtifactID,
                                    sqlParam); //I'll need to figure out the db connection for this to work.
                            while (reader.Read())
                            {
                                var guidValue =
                                    reader.GetGuid(
                                        0); //This reads the results of the the SQL query into a string to work with in VS.
                                guidBox.Text = guidValue.ToString(); //Sets guidBox to the GUID found.
                            }
                            reader.Close();
                            //Begin ops for SQL query to find the name that goes along with the artifactID
                            string sqlArtifactName = "SELECT TextIdentifier FROM " + tableInfoArtifactEDDS +
                                                     " WHERE ArtifactID = @objectString";
                            var readerName =
                                contextDB.ExecuteParameterizedSQLStatementAsReader(sqlArtifactName,
                                    sqlParam); //I'll need to figure out the db connection for this to work.
                            while (readerName.Read())
                            {
                                var nameValue =
                                    readerName.GetString(
                                        0); //This reads the results of the the SQL query into a string to work with in VS.
                                aIDNameBox.Text = nameValue; //Sets guidBox to the GUID found.
                            }
                            readerName.Close();
                        }
#endregion


                        #region NOT ADMIN LEVEL!
                    }
                    else
                    {
                        contextDB = GetDBContext(Convert.ToInt32(workspaceString));
                        var sqlParams = new List<SqlParameter>
                        {
                            new SqlParameter("@workspaceString", workspaceString),
                            new SqlParameter("@objectString", objectString)
                        };


                        //  bool resultWorkspace = workspaceString.Any(x => !char.IsLetter(x)); //Check for alphabet chars in string
                        bool
                            resultObject =
                                objectString.Any(x =>
                                    !char.IsDigit(
                                        x)); //Check for alphabet chars in string //switched from using IsChar to test isDigit - I just want to be able to separate artiID and Name input.

                        if (resultObject) // then we are working with a string
                        {
                            string sqlString = "SELECT b.ArtifactGuid, a.ArtifactID from " + tableInfoArtifact +
                                               " a INNER JOIN " + tableInfoArtifactGuid +
                                               " b ON a.ArtifactID = b.ArtifactID WHERE a.TextIdentifier = @objectString";
                            //If inputstring is a string this means that we are working with a name NOT
                            //an ArtifactID
                            var reader =
                                contextDB.ExecuteParameterizedSQLStatementAsReader(sqlString,
                                    sqlParams); 
                            while (reader.Read())
                            {
                                var artifactIDVal = reader.GetInt32(1);
                                ;
                                var guidValue =
                                    reader.GetGuid(
                                        0); //This reads the results of the the SQL query into a string to work with in VS.
                                aIDNameBox.Text = artifactIDVal.ToString();
                                guidBox.Text = (guidValue.ToString()); //Sets guidBox to the GUID found.
                            }
                            reader.Close();
                            //Get Object Name
                        }
                        else
                        {
                            var sqlParam = new List<SqlParameter>
                            {
                                new SqlParameter("@workspaceString", workspaceString),
                                new SqlParameter("@objectString", objectString)

                            };


                            //If inputstring is NOT a string this means we are working with an artifactID 
                            //MOST LIKELY. Someone could have named something will all numbers. Refine this
                            //logic at a later date. Also refine the logic for dealing with admin instace

                            string sqlArtifactID = "SELECT [ArtifactGuid] FROM " + tableInfoArtifactGuid +
                                                   " WHERE [ArtifactID] = @objectString";
                            var reader =
                                contextDB.ExecuteParameterizedSQLStatementAsReader(sqlArtifactID,
                                    sqlParam); //I'll need to figure out the db connection for this to work.
                            while (reader.Read())
                            {
                                var guidValue =
                                    reader.GetGuid(
                                        0); //This reads the results of the the SQL query into a string to work with in VS.
                                guidBox.Text = guidValue.ToString(); //Sets guidBox to the GUID found.
                            }
                            reader.Close();
                            //Begin ops for SQL query to find the name that goes along with the artifactID
                            string sqlArtifactName = "SELECT TextIdentifier FROM " + tableInfoArtifact +
                                                     " WHERE ArtifactID = @objectString";
                            var readerName =
                                contextDB.ExecuteParameterizedSQLStatementAsReader(sqlArtifactName,
                                    sqlParam); //I'll need to figure out the db connection for this to work.
                            while (readerName.Read())
                            {
                                var nameValue =
                                    readerName.GetString(
                                        0); //This reads the results of the the SQL query into a string to work with in VS.
                                aIDNameBox.Text = nameValue; //Sets guidBox to the GUID found.
                            }
                            readerName.Close();
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                guidBox.Text = ex.ToString();
            }
        }
        public IDBContext GetDBContext(int caseID)
        {
            kCura.Data.RowDataGateway.Context context =
                new kCura.Data.RowDataGateway.Context("server=VM-T002-Sandbox\\SQLEDDS01; user=eddsdbo;password=Test1234!");//("server=192.168.137.96;user=eddsdbo;password=Test1234!;");
            return new DBContext(context);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
