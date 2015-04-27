namespace AutoskolaWeb.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aslaspapsfa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AnswerText = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        CreatedByUser = c.String(),
                        ModifiedByUser = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuestionText = c.String(),
                        CorrectAnswer = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        CreatedByUser = c.String(),
                        ModifiedByUser = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuizName = c.String(nullable: false),
                        StartTime = c.DateTime(),
                        Duration = c.Int(nullable: false),
                        EndTime = c.DateTime(),
                        QuizPoints = c.Int(nullable: false),
                        ResultsID = c.Int(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        CreatedByUser = c.String(),
                        ModifiedByUser = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Results", t => t.ResultsID)
                .Index(t => t.ResultsID);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        DurationQuiz = c.DateTime(nullable: false),
                        Score = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        CreatedByUser = c.String(),
                        ModifiedByUser = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Surname = c.String(),
                        Autoskola = c.String(),
                        AutoskolaCode = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.QuestionAnswers",
                c => new
                    {
                        Question_ID = c.Int(nullable: false),
                        Answer_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Question_ID, t.Answer_ID })
                .ForeignKey("dbo.Questions", t => t.Question_ID, cascadeDelete: true)
                .ForeignKey("dbo.Answers", t => t.Answer_ID, cascadeDelete: true)
                .Index(t => t.Question_ID)
                .Index(t => t.Answer_ID);
            
            CreateTable(
                "dbo.QuizQuestions",
                c => new
                    {
                        Quiz_ID = c.Int(nullable: false),
                        Question_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Quiz_ID, t.Question_ID })
                .ForeignKey("dbo.Quizs", t => t.Quiz_ID, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.Question_ID, cascadeDelete: true)
                .Index(t => t.Quiz_ID)
                .Index(t => t.Question_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Quizs", "ResultsID", "dbo.Results");
            DropForeignKey("dbo.QuizQuestions", "Question_ID", "dbo.Questions");
            DropForeignKey("dbo.QuizQuestions", "Quiz_ID", "dbo.Quizs");
            DropForeignKey("dbo.QuestionAnswers", "Answer_ID", "dbo.Answers");
            DropForeignKey("dbo.QuestionAnswers", "Question_ID", "dbo.Questions");
            DropIndex("dbo.QuizQuestions", new[] { "Question_ID" });
            DropIndex("dbo.QuizQuestions", new[] { "Quiz_ID" });
            DropIndex("dbo.QuestionAnswers", new[] { "Answer_ID" });
            DropIndex("dbo.QuestionAnswers", new[] { "Question_ID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Quizs", new[] { "ResultsID" });
            DropTable("dbo.QuizQuestions");
            DropTable("dbo.QuestionAnswers");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Results");
            DropTable("dbo.Quizs");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
