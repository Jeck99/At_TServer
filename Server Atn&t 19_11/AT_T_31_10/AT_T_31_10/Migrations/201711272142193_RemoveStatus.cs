namespace AT_T_31_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveStatus : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Applicants", "Status");
            DropColumn("dbo.Jobs", "Active");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applicants", "Status", c => c.String());
        }
    }
}
