namespace AT_T_31_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppAndJobSkill : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicantSkillsets",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ApplicantId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobSkillsets",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.JobSkillsets");
            DropTable("dbo.ApplicantSkillsets");
        }
    }
}
