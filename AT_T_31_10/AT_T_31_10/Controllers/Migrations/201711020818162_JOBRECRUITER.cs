namespace AT_T_31_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JOBRECRUITER : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicants", "Title");
        }
    }
}
