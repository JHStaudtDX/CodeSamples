    public static void CreateProject()
    {
        Guid LegalHoldTypeChoiceGuid = new Guid("79222915-9A4F-4C0A-A701-F8BE87E31706");
        Guid AnswerYesNoChoiceGuid = new Guid("F724D1D2-FBCF-4070-8509-28663C44B00A");

        try
        {
            ConnectionManager cmr = new ConnectionManager();
            using (var proxy = cmr.GetRsapiClient())
            {
                proxy.APIOptions.WorkspaceID = 1038380;
                var matterToCreate = new NewMatter()
                {
                    NameR = "LH Project Example",
                    OwnerEmailR = "UserEmail@EmailAddress.Com",
                    FavoriteR = AnswerYesNoChoiceGuid,
                    UseAsTemplateR = AnswerYesNoChoiceGuid,
                    Owner = "User Email",
                    Type = LegalHoldTypeChoiceGuid,
                    SubjectMatterSD = DateTime.Now,
                    SubjectMatterED = DateTime.Now,
                    GeneralCounsel = "General Counsel",
                    ExternalCounsel = "External Counsel",
                    Description = "This is a project"
                };

                RDO test = new RDO();
                test.ArtifactTypeID = 1000048;
                test.Fields.Add(new FieldValue("Name", matterToCreate.NameR));
                test.Fields.Add(new FieldValue("Owner Email", matterToCreate.OwnerEmailR));
                test.Fields.Add(new FieldValue("Favorite", false));
                test.Fields.Add(new FieldValue("Use as Template", false));
                test.Fields.Add(new FieldValue("Project Owner", matterToCreate.Owner));
                test.Fields.Add(new FieldValue("Type", matterToCreate.Type));
                test.Fields.Add(new FieldValue("Subject Matter Start Date", matterToCreate.SubjectMatterSD));
                test.Fields.Add(new FieldValue("Subject Matter End Date", matterToCreate.SubjectMatterED));
                test.Fields.Add(new FieldValue("General Counsel", matterToCreate.GeneralCounsel));
                test.Fields.Add(new FieldValue("External Counsel", matterToCreate.ExternalCounsel));
                test.Fields.Add(new FieldValue("Project Description", matterToCreate.Description));

                try
                {
                    var resultsSet = proxy.Repositories.RDO.Create(test);
                    if (!resultsSet.Success)
                    {
                        Console.WriteLine("Failed to create new project.");

                    }
                    else { Console.WriteLine("Successfully created new project."); }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public class NewMatter
    {
        public string NameR { get; set; }
        public string Owner { get; set; }
        public Guid Type { get; set; }
        public string OwnerEmailR { get; set; }
        public DateTime SubjectMatterSD { get; set; }
        public DateTime SubjectMatterED { get; set; }
        public Guid FavoriteR { get; set; }
        public Guid UseAsTemplateR { get; set; }
        public string GeneralCounsel { get; set; }
        public string ExternalCounsel { get; set; }
        public string Description { get; set; }
    }


