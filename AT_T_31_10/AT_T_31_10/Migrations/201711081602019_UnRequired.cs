namespace AT_T_31_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Applicants", "Title", c => c.String());
            AlterColumn("dbo.Applicants", "Name", c => c.String());
            AlterColumn("dbo.Applicants", "Email", c => c.String());
            AlterColumn("dbo.Applicants", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Applicants", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Applicants", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Applicants", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Applicants", "Title", c => c.String(nullable: false));
        }
    }
}
