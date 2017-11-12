namespace AT_T_31_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppAndJobSkil : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Skillsets", "Name", c => c.String());
            AlterColumn("dbo.Skillsets", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Skillsets", "Description", c => c.Int(nullable: false));
            AlterColumn("dbo.Skillsets", "Name", c => c.Int(nullable: false));
        }
    }
}
