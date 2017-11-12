namespace AT_T_31_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicanChange : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Applicants");
            CreateTable(
                "dbo.ApplicantRecruiters",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ApplicantId = c.Int(nullable: false),
                        RecruiterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Applicants", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Applicants", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Applicants");
            AlterColumn("dbo.Applicants", "Id", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.ApplicantRecruiters");
            AddPrimaryKey("dbo.Applicants", "Id");
        }
    }
}
