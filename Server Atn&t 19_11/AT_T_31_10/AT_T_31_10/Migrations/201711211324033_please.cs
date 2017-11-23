namespace AT_T_31_10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class please : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "Status", c => c.String());
            AlterColumn("dbo.Reviews", "Content", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Reviews", "Status", c => c.String(nullable: false));
        }
    }
}
