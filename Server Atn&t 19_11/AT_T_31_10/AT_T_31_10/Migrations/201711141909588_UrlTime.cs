namespace AT_T_31_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UrlTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "Url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicants", "Url");
        }
    }
}
