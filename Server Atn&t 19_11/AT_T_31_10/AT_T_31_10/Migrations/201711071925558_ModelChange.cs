namespace AT_T_31_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Applicants", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Applicants", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Applicants", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Applicants", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Jobs", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Managers", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.Managers", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Managers", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Reviews", "Status", c => c.String(nullable: false));
            AlterColumn("dbo.Reviews", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Skillsets", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Skillsets", "Name", c => c.String());
            AlterColumn("dbo.Reviews", "Content", c => c.String());
            AlterColumn("dbo.Reviews", "Status", c => c.String());
            AlterColumn("dbo.Managers", "Password", c => c.String());
            AlterColumn("dbo.Managers", "Email", c => c.String());
            AlterColumn("dbo.Managers", "UserName", c => c.String());
            AlterColumn("dbo.Jobs", "Title", c => c.String());
            AlterColumn("dbo.Applicants", "Phone", c => c.String());
            AlterColumn("dbo.Applicants", "Email", c => c.String());
            AlterColumn("dbo.Applicants", "Title", c => c.String());
            AlterColumn("dbo.Applicants", "Name", c => c.String());
        }
    }
}
