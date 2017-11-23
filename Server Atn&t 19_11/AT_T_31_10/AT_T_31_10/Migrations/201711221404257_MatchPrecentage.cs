namespace AT_T_31_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatchPrecentage : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Applicants");
            DropPrimaryKey("dbo.JobRecruiters");
            AlterColumn("dbo.Applicants", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.JobRecruiters", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ManagerLogins", "UserId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Applicants", "Id");
            AddPrimaryKey("dbo.JobRecruiters", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.JobRecruiters");
            DropPrimaryKey("dbo.Applicants");
            AlterColumn("dbo.ManagerLogins", "UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.JobRecruiters", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Applicants", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.JobRecruiters", "Id");
            AddPrimaryKey("dbo.Applicants", "Id");
        }
    }
}
