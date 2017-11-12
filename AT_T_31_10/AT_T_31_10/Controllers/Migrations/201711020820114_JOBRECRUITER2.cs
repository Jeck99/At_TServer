namespace AT_T_31_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JOBRECRUITER2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobRecruiters",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        RecruiterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JobRecruiters");
        }
    }
}
