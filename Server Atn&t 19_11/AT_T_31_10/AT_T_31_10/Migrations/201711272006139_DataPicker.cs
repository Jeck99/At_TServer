namespace AT_T_31_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataPicker : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "Position", c => c.String());
            AddColumn("dbo.Reviews", "InterviewDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Applicants", "Experience", c => c.Single(nullable: false));
            AlterColumn("dbo.Jobs", "Experience", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "Experience", c => c.Int(nullable: false));
            AlterColumn("dbo.Applicants", "Experience", c => c.Int(nullable: false));
            DropColumn("dbo.Reviews", "InterviewDate");
            DropColumn("dbo.Applicants", "Position");
        }
    }
}
