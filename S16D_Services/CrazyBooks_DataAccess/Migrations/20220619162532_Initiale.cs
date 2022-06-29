using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CrazyBooks_DataAccess.Migrations
{
    public partial class Initiale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Biography = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Speciality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PublisherSite = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorDetail_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Author_AuthorDetail_AuthorDetail_Id",
                        column: x => x.AuthorDetail_Id,
                        principalTable: "AuthorDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Promo = table.Column<bool>(type: "bit", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Resume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject_Id = table.Column<int>(type: "int", nullable: false),
                    Publisher_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_Publisher_Publisher_Id",
                        column: x => x.Publisher_Id,
                        principalTable: "Publisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Subject_Subject_Id",
                        column: x => x.Subject_Id,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    Book_Id = table.Column<int>(type: "int", nullable: false),
                    Author_Id = table.Column<int>(type: "int", nullable: false),
                    PCRoyalties = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.Author_Id, x.Book_Id });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Author_Author_Id",
                        column: x => x.Author_Id,
                        principalTable: "Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Book_Book_Id",
                        column: x => x.Book_Id,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "Id", "AuthorDetail_Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 7, null, "Elie", "Hanson" },
                    { 8, null, "Agatha", "Christie" },
                    { 9, null, "Josh", "Malerman" },
                    { 10, null, "Julie", "Laganière" },
                    { 11, null, "Hugues", "Chassé" },
                    { 12, null, "Insaf Ben", "Kadni" }
                });

            migrationBuilder.InsertData(
                table: "AuthorDetail",
                columns: new[] { "Id", "Biography", "Photo" },
                values: new object[,]
                {
                    { 7, "Josh Malerman est le chanteur et le parolier du groupe de rock The High Strung. 'Bird Box', son premier roman a remporté le Michigan Notable Book Award. Il habite à Ferndale dans le Michigan.", "" },
                    { 5, "Kim Messier enseigne le français à l'école secondaire du Verbe Divin, à Granby, où elle habite. En publiant son premier roman aux Éditions de Mortagne, cette enseignante réalise le plus grand rêve de sa vie ! Le récit de Léa s'est imposé à elle en 2008, lors d'un congé de maternité. Depuis, c'est l'avalanche ! Les idées s'enchaînent, les mots surgissent et Kim ne rêve plus que d'une chose : écrire à temps plein.", "" },
                    { 4, "musicien et enseignant. Il a joué pour Cavalia et dans des groupes de trad, de free jazz et de black métal. 'Le puits' (2020) est son premier roman.", "" },
                    { 6, "Né le31 août 1964, est un écrivain québécois spécialisé en fantastique et fantasy. Son premier roman, L'Orbe et le Croissant, est publié par les Éditions Arion en 2006.Les Éditions Porte Bonheur l'ont réédité, puis édité les deux tomes suivants formant La Trilogie de L'Orbe.Il a ensuite écrit une deuxième trilogie L'héritière de Ferrolia en 2010. Auteur prolifique, il a ensuite publié Cœur de givre en avril 2011 et poursuivi avec une incursion dans le monde de la science-fiction en livrant un tome de la série des Clowns vengeurs, Valse macabre avril 2012. Sa dernière œuvre, Les âmes perdues, est parue en novembre 2012.", "" },
                    { 2, "Auteur de plusieurs ouvrages portant sur l’administration, la gestion des ressources humaines, l’informatique et le commerce électronique, Bernard Turgeon a obtenu le Prix du ministre de l’Éducation(1982), deux mentions(1987, 2000) et un prix d’encouragement(1985).Titulaire d’un baccalauréat en administration et d’une licence en sciences commerciales, il enseigne le management et la gestion des ressources humaines au Cégep Édouard - Montpetit", "" },
                    { 1, "Agatha Christie est sans nul doute l'une des romancières les plus appréciées de son temps. Auteure de quatre-vingt-quatre ouvrages qui constituent pour la plupart des intrigues policières, d'une vingtaine de pièces de théâtre et de plusieurs recueils de nouvelles, elle est parvenue à faire de ses oeuvres de grands succès du XXe siècle, lues partout dans le monde (plus de 2 milliards d'exemplaires vendus).", "" },
                    { 3, "Docteur en relations industrielles, Dominique Lamaute enseigne au département des Techniques administratives du Cégep de Saint-Hyacinthe. Auteur d’ouvrages sur le management, la gestion des ressources humaines et l’entreprise, il a obtenu une mention au Prix du ministre de l’Éducation (2000). Spécialisé en gestion stratégique des ressources humaines, Dominique Lamaute est appelé à prononcer plusieurs conférences sur l’évolution des modèles de gestion des ressources humaines et sur le talent management.", "" }
                });

            migrationBuilder.InsertData(
                table: "Publisher",
                columns: new[] { "Id", "Name", "PublisherSite", "Speciality" },
                values: new object[,]
                {
                    { 1, "Livre de poche", "https://www.livredepoche.com/", "Policier, Mystère" },
                    { 2, "Chenelière", "https://www.cheneliere.ca/", "Études supérieures" },
                    { 3, "ADA", "https://www.ada-inc.com/", "jeunesse, fantastique" }
                });

            migrationBuilder.InsertData(
                table: "Subject",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 6, "Fantastique" },
                    { 1, "Policier" },
                    { 2, "Science-Fiction" },
                    { 3, "Biographie" },
                    { 4, "Romance" },
                    { 5, "Collégial-universitaire" },
                    { 7, "Horreur" }
                });

            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "Id", "AuthorDetail_Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, 1, "Agatha", "Christie" },
                    { 2, 2, "Bernard", "Turgeon" },
                    { 3, 3, "Dominique", "Lamaute" },
                    { 4, 4, "Vincent", "Founier-Boivert" },
                    { 5, 5, "Kim", "Messier" },
                    { 6, 6, "Guy", "Bergeron" },
                    { 13, 7, "Josh", "Malerman" }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "Available", "ISBN", "Price", "Promo", "PublishedDate", "Publisher_Id", "Resume", "Subject_Id", "Title" },
                values: new object[,]
                {
                    { 5, true, "9782923898902", 10m, true, new DateTime(2014, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Culcuth, dieu de la mort, fomente en secret un plan visant à contrôler l’ensemble de l’humanité afin d’augmenter son pouvoir. Il crée le Portail des ombres, d’où s’échappent ses créatures. Pendant ce temps, au royaume de Ferrolia, Miranda est atteinte dès sa naissance d’une grave maladie qui provoque des crises incontrôlables. Son père, un roi despote, tente de la sacrifier afin de regagner la faveur des dieux. Elle sera sauvée par Torkil, un garde de la reine qui l’élèvera loin de Ferrolia. Âgée d’environ douze ans, Miranda que l’on nomme maintenant Servia afin de dissimuler son identité, perd Torkil qui meurt suite à une attaque à sa maison, avant qu’il ait pu lui dévoiler ses origines. Aidée d’un ami nommé Keiko qui possède la faculté de se changer en ours, elle tentera de trouver un ermite qui pourrait peut-être la guérir des crises qui la terrassent toujours. Mais Thamir l’ermite ne parvient pas à la guérir. Avec lui, ils se retrouveront tout près du Portail des Ombres et ils devront combattre les créatures de Culcuth qui ont commencé à envahir la région. Ils rencontreront un peuple au physique d’enfants, mais qui est puissant en magie, les Juventis. Les trois compagnons sont faits prisonniers et Culcuth force Thamir à devenir son héraut. Servia et Keiko parviendront à s’échapper avec l’aide des Juventis et de Thamir qui résistera au dieu, mais se sacrifiera afin de détruire le Portail des Ombres.", 6, "Le portail des ombres" },
                    { 4, true, "9782897655044", 22m, false, new DateTime(1921, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "La Dame Blanche charge Servia et Keiko de mettre sur pied une organisation pour maintenir l’équilibre sur Arménis, l’Ordre de l’épervier. Devenue reine malgré elle, Servia devra se préparer à une guerre dont l’issue est incertaine. Quelques années plus tard, les héros seront conviés à une mission ultime dont le résultat bouleversera tout, même les dieux.", 6, "Le règne de l'épervier" },
                    { 3, true, "9782897655013", 20m, false, new DateTime(2020, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Dans le panthéon des divinités d’Arménis, la discorde règne. La Dame Blanche, mère de tous les dieux, décide de noyer le monde sous un déluge. Les humains comme les dieux seront soumis à différentes épreuves. Servia et Keiko porteront ce poids pour les humains à leur insu. Servia, toujours accompagné de Keiko, tentera de percer les mystères de son passé.", 6, "Ldame Blanche" },
                    { 15, true, "9782765076803", 60m, true, new DateTime(2121, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "La cinquième édition de cet ouvrage expose de manière concrète et concise les grands thèmes de la gestion des ressources humaines en tenant compte d’une préoccupation actuelle: la pénurie de main-d’oeuvre. Le contenu théorique et pratique, entièrement mis à jour, illustre les diverses stratégies à appliquer dans ce contexte.", 5, "Supervision gestion des RH" },
                    { 11, true, "9782898032752", 20m, false, new DateTime(2019, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Le Menvatt tourna une petite clé située sur le côté de la boîte avant de la poser au sol et d’en ouvrir le couvercle. Une musique triste se mit à résonner dans la pièce, et au centre de la boîte, la figurine d’une ballerine entièrement vêtue de blanc tournoyait au bras d’un clown multicolore. Jordan pencha la tête de côté, fredonnant la mélodie. Il se pencha de nouveau au-dessus de son sac, d’où il sortit une paire de pinces robustes au bout pointu. Mercado fronça les sourcils. Les menvatts ne faisaient pas dans la dentelle. Ils tuaient, appliquaient la vengeance sans perdre de temps. Pourquoi alors cette paire de pinces? Dans la Quadri-métropole aux prises avec des luttes politiques, les arcurides du Gouvernement Légitime pourchassent sans relâche les Odi-Menvatts, alors qu’un dangereux psychopathe s’est glissé parmi eux…", 2, "Valse macabre" },
                    { 10, false, "9782898032813", 21m, true, new DateTime(2019, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Mi-Panthère des neiges, mi-homme, Uncia est une Anomalie, un descendant des êtres humains ayant subi des expérimentations génétiques. Élevé dans le plus grand des secrets par les Adoratrices d’Odi, le menvatt à la canne au pommeau d’or accomplit maintes missions pour le compte des moniales, recluses dans le Grand Nord. Lorsqu’il se voit confier la mission d’éliminer une scientifique qui teste ses expériences sur le marché noir, Uncia découvre que cette femme détient la clé pour effectuer de nouvelles modifications génétiques capables de mettre fin à la domination des Arcurides et au système de castes. Pour exploiter sa découverte et la protéger, l’Anomalie entraîne sa cible chez les moniales. Marqué par des années d’exclusion et d’obéissance, Uncia se liera pour la première fois de sa vie à une femelle, dont il devra assurer la protection lorsqu’elle se retrouvera à la tête d’une rébellion de masse contre le Gouvernement légitime.", 2, "Uncia" },
                    { 9, true, "9782898190001", 20m, false, new DateTime(2020, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Saint-Hyacinthe, 1996 Une vache est retrouvée décapitée près d'un puits. La sous-lieutenant Monique Demers et son patron Réal Rondeau, inspecteur bougon à deux doigts du divorce, sont en charge du singulier dossier. L'enquête, d'abord banale, débouche sur la disparition du fils du propriétaire de l'animal. Rondeau peine à démêler les fils de l'histoire. Pourquoi y a-t-il une quantité astronomique de PCP dans l'estomac de la vache étêtée? Et quel est le lien entre le jeune disparu et le réseau de revente de stupéfiants oeuvrant tout près de la polyvalente de la ville? Un roman policier prenant qui explore les vices les plus obscurs d'un coin de province moins tranquille qu'il en a l'air...", 1, "Le puits" },
                    { 13, false, "2253079391", 12m, false, new DateTime(1937, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "La séduisante Linnet Ridgeway est retrouvée tuée d'une balle dans la tête lors d'une croisière sur le Nil. Chacun des passagers ayant au moins une bonne raison d'avoir assassiné la riche Américaine, l'enquête n'en est que plus difficile pour le célèbre détective belge Hercule Poirot.", 1, "Mort sur le Nil" },
                    { 12, true, "9782898190063", 20m, true, new DateTime(2020, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Alain Thibault, éminent conférencier du paranormal, est approché par une mystérieuse organisation pour participer à une expédition dans l'Arctique. Sous la menace des responsables de l'institution, il est contraint d'accepter de partir dans le Grand Nord canadien. Pourquoi la participation d'Alain est-elle essentielle pour les membres de cette mission ? Quels dangers guettent les participants de l'expédition Mirage ? Que cherche cette énigmatique agence au-delà du 82e parallèle ? Lorsque l'expédition découvre une base souterraine construite par les nazis durant la Deuxième Guerre mondiale, Alain comprend qu'on ne lui a vraiment pas tout dit... Un thriller fantastique haletant nous transportant aux confins de l'Arctique, abri sauvage et méconnu de très anciens secrets...", 1, "Mission Arctik" },
                    { 8, true, "99782896389100", 20m, false, new DateTime(2021, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Alain, un artiste peintre québécois, tombe un jour sur le vieux et mystérieux carnet de souvenirs de son grand-oncle Henri. Celui-ci, interné depuis son retour de France, où il avait assisté en tant que jeune soldat aux horreurs de la Première Guerre mondiale, semblait depuis longtemps porter un lourd secret sur ses épaules. Alain décide donc d’en savoir plus sur ces écrits troublants.Son enquête le mène en France,où il fera la connaissance d’un conférencier écossais un peu farfelu qui semble avancer les mêmes élucubrations que Henri.S’agit - il d’une pure fabulation, ou bien existe - t - il un lien entre ces deux témoignages ??", 6, "Le carnet maudit" },
                    { 7, true, "9782253241782", 12m, true, new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Les dix invités sont arrivés sur l'île du Nègre, mais rien ne semble normal : leur hôte est absent et quelqu'un a déposé dans leur chambre une comptine intitulée Les dix petits Nègres. Tout bascule quand une voix accuse chacun des invités d'un crime.", 1, "Inspection" },
                    { 6, true, "9782253241782", 10m, false, new DateTime(1921, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Les dix invités sont arrivés sur l'île du Nègre, mais rien ne semble normal : leur hôte est absent et quelqu'un a déposé dans leur chambre une comptine intitulée Les dix petits Nègres. Tout bascule quand une voix accuse chacun des invités d'un crime.", 1, "Ils étaient dix" },
                    { 1, true, "9782253167129", 22m, false, new DateTime(1920, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Qui avait intérêt à assassiner Mrs Ingelthorp, la richissime propriétaire du domaine de Styles? Pratiquement tous ceux qui l’entouraient. Qui est le coupable idéal ? Le second et jeune mari de la victime… ", 1, "Affaires de famille" },
                    { 14, false, "9782253258032", 13m, false, new DateTime(1920, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rassemble quatorze nouvelles policières mettant en scène le détective Parker Pyne, professeur de bonheur, prêt à tout pour rendre ses clients heureux.", 1, "Parker Pyne enquête" },
                    { 2, true, "9782253183839", 14m, true, new DateTime(2019, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Malorie et ses enfants sont barricadés chez eux. Dehors, il y a un danger terrible. Ils ne peuvent sortir que les yeux bandés pour rester en vie. Mais le temps est compté, alors Malorie décide de réveiller ses enfants afin de partir à la recherche d'une hypothétique colonie de survivants. Premier roman.", 7, "BirdBox" }
                });

            migrationBuilder.InsertData(
                table: "AuthorBook",
                columns: new[] { "Author_Id", "Book_Id", "PCRoyalties" },
                values: new object[,]
                {
                    { 1, 1, 100 },
                    { 1, 6, 100 },
                    { 13, 7, 100 },
                    { 4, 9, 100 },
                    { 7, 12, 100 },
                    { 1, 13, 100 },
                    { 1, 14, 100 },
                    { 5, 10, 100 },
                    { 6, 11, 100 },
                    { 2, 15, 60 },
                    { 3, 15, 40 },
                    { 6, 3, 100 },
                    { 6, 4, 100 },
                    { 6, 5, 100 },
                    { 7, 8, 100 },
                    { 13, 2, 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Author_AuthorDetail_Id",
                table: "Author",
                column: "AuthorDetail_Id",
                unique: true,
                filter: "[AuthorDetail_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_Book_Id",
                table: "AuthorBook",
                column: "Book_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Publisher_Id",
                table: "Book",
                column: "Publisher_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Subject_Id",
                table: "Book",
                column: "Subject_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "AuthorDetail");

            migrationBuilder.DropTable(
                name: "Publisher");

            migrationBuilder.DropTable(
                name: "Subject");
        }
    }
}
