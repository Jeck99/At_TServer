namespace AT_T_31_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JOBRECRUITER4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Jobs", "RecruiterId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "RecruiterId", c => c.Int(nullable: false));
        }
    }
}
