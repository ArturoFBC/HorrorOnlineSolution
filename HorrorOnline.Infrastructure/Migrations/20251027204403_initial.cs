using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HorrorOnline.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    StoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", maxLength: 40000, nullable: true),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateUploaded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.StoryId);
                    table.ForeignKey(
                        name: "FK_Stories_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookMarks",
                columns: table => new
                {
                    BookMarkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MarkedLocation = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookMarks", x => x.BookMarkId);
                    table.ForeignKey(
                        name: "FK_BookMarks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookMarks_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "StoryId");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    ReviewText = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Stories_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Stories",
                        principalColumn: "StoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoryTag",
                columns: table => new
                {
                    StoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryTag", x => new { x.StoryId, x.TagId });
                    table.ForeignKey(
                        name: "FK_StoryTag_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "StoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoryTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("3401faeb-e34e-492b-af07-37dc9b6e1bb1"), 0, "A040D128-3A7C-4B0A-93AB-71130EAF5F31", null, false, false, null, null, "EMILIA PARDO BAZAN", null, null, false, null, false, "Emilia Pardo Bazán" },
                    { new Guid("5f8dff58-8fcd-46d2-bc95-90a73e9f9be3"), 0, "BA9FE8F2-3343-49EC-A4F7-934047A5D59A", null, false, false, null, null, "GUSTAVO ADOLFO BECQUER", null, null, false, null, false, "Gustavo Adolfo Bécquer" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "TagId", "TagName" },
                values: new object[,]
                {
                    { new Guid("43559433-ea81-43c5-8501-f334575fc00c"), "sobrenatural" },
                    { new Guid("d28c16f7-dbe3-4273-ba0e-b4252f1c11b8"), "lovecraftiano" },
                    { new Guid("fd4cf16b-8c77-4371-a0d3-0bcf0a70d031"), "romance" }
                });

            migrationBuilder.InsertData(
                table: "Stories",
                columns: new[] { "StoryId", "AuthorId", "DateUploaded", "Summary", "Text", "Title" },
                values: new object[,]
                {
                    { new Guid("728070cf-8796-4e71-bda7-aaae404278bf"), new Guid("5f8dff58-8fcd-46d2-bc95-90a73e9f9be3"), new DateTime(2025, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Una doncella reta sutilmente a un caballero que la corteja a ir al siniestro Monte de las Ánimas, y éste acepta el desafío.", "La noche de difuntos me despertó, a no sé qué hora, el doble de las campanas; su tañido monótono y eterno me trajo a las mientes esta tradición que oí hace poco en Soria.\nIntenté dormir de nuevo; ¡imposible! Una vez aguijoneada, la imaginación es un caballo que se desboca, y al que no sirve tirarle de la rienda. Por pasar el rato, me decidí a escribirla, como, en efecto, lo hice.\nYo no la oí en el mismo lugar en que acaeció, y la he escrito volviendo algunas veces la cabeza, con miedo cuando sentía crujir los cristales de mi balcón, estremecidos por el aire frío de la noche.\nSea de ello lo que quiera, ahí va, como el caballo de copas.\n- I -\n-Atad los perros; haced la señal con las trompas para que se reúnan los cazadores, y demos la vuelta a la ciudad. La noche se acerca, es día de Todos los Santos y estamos en el Monte de las Ánimas.\n-¡Tan pronto!\n-A ser otro día no dejara yo de concluir con ese rebaño de lobos que las nieves del Moncayo han arrojado de sus madrigueras; pero hoy es imposible. Dentro de poco sonará la oración en los Templarios, y las ánimas de los difuntos comenzarán a tañer su campana en la capilla del monte.\n-¡En esa capilla ruinosa! ¡Bah! ¿Quieres asustarme?\n-No, hermosa prima; tú ignoras cuanto sucede en este país, porque aún no hace un año que has venido a él desde muy lejos. Refrena tu yegua; yo también pondré la mía al paso, y mientras dure el camino te contaré la historia.\nLos pajes se reunieron en alegres y bulliciosos grupos; los condes de Borges y de Alcudiel montaron en sus magníficos caballos, y todos juntos siguieron a sus hijos Beatriz y Alonso, que precedían la comitiva a bastante distancia.\nMientras duraba el camino, Alonso narró en estos términos la prometida historia:\n«Ese monte que hoy llaman de las Ánimas pertenecía a los Templarios, cuyo convento ves allí, a la margen del río. Los Templarios eran guerreros y religiosos a la vez. Conquistada Soria a los árabes, el rey los hizo venir de lejanas tierras para defender la ciudad por la parte del puente, haciendo en ello notable agravio a sus nobles de Castilla, que así hubieran sabido solos defenderla como solos la conquistaron.\n»Entre los caballeros de la nueva y poderosa orden y los hidalgos de la ciudad fermentó por algunos años, y estalló al fin, un odio profundo. Los primeros tenían acotado ese monte, donde reservaban caza abundante para satisfacer sus necesidades y contribuir a sus placeres; los segundos determinaron organizar una gran batida en el coto, a pesar de las severas prohibiciones de los clérigos con espuelas, como llamaban a sus enemigos.\n»Cundió la voz del reto, y nada fue parte a detener a los unos en su manía de cazar y a los otros en su empeño de estorbarlo. La proyectada expedición se llevó a cabo. No se acordaron de ella las fieras; antes la tendrían presente tantas madres como arrastraron sendos lutos por sus hijos. Aquello no fue una cacería, fue una batalla espantosa: el monte quedó sembrado de cadáveres; los lobos, a quienes se quiso exterminar, tuvieron un sangriento festín. Por último, intervino la autoridad del rey; el monte, maldita ocasión de tantas desgracias, se declaró abandonado, y la capilla de los religiosos, situada en el mismo monte, y en cuyo atrio se enterraron juntos amigos y enemigos, comenzó a arruinarse.\n»Desde entonces dicen que, cuando llega la noche de Difuntos, se oye doblar sola la campana de la capilla, y que las ánimas de los muertos, envueltas en jirones de sus sudarios, corren como en una cacería fantástica por entre las breñas y los zarzales. Los ciervos braman espantados, los lobos aúllan, las culebras dan horrorosos silbidos, y al otro día se han visto impresas en la nieve las huellas de los descarnados pies de los esqueletos. Por eso en Soria le llamamos el Monte de las Ánimas, y por eso he querido salir de él antes que cierre la noche».\nLa relación de Alonso concluyó justamente cuando los dos jóvenes llegaban al extremo del puente que da paso a la ciudad por aquel lado. Allí esperaron al resto de la comitiva, la cual, después de incorporársele los dos jinetes, se perdió por entre las estrechas y oscuras calles de Soria.\n- II -\nLos servidores acababan de levantar los manteles; la alta chimenea gótica del palacio de los condes de Alcudiel despedía un vivo resplandor, iluminando algunos grupos de damas y caballeros que alrededor de la lumbre conversaban familiarmente, y el viento azotaba los emplomados vidrios de las ojivas del salón.\nSólo dos personas parecían ajenas a la conversación general: Beatriz y Alonso. Beatriz seguía con los ojos, absortos en un vago pensamiento, los caprichos de la llama. Alonso miraba el reflejo de la hoguera chispear en las azules pupilas de Beatriz.\nAmbos guardaban hacía rato un profundo silencio.\nLas dueñas referían, a propósito de la noche de Difuntos, cuentos tenebrosos en que los espectros y los aparecidos representaban el principal papel, y las campanas de las iglesias de Soria doblaban a lo lejos con un tañido monótono y triste.\n-Hermosa prima -exclamó al fin Alonso rompiendo el largo silencio en que se encontraban-: pronto vamos a separarnos, tal vez para siempre; las áridas llanuras de Castilla, sus costumbres toscas y guerreras, sus hábitos sencillos y patriarcales sé que no te gustan; te he oído suspirar varias veces, acaso por algún galán de tu lejano señorío.\nBeatriz hizo un gesto de fría indiferencia; todo su carácter de mujer se reveló en aquella desdeñosa contracción de sus delgados labios.\n-Tal vez por la pompa de la corte francesa, donde hasta aquí has vivido -se apresuró a añadir el joven-. De un modo o de otro, presiento que no tardaré en perderte... Al separarnos, quisiera que llevases una memoria mía... ¿Te acuerdas cuando fuimos al templo a dar gracias a Dios por haberte devuelto la salud que viniste a buscar a esta tierra? El joyel que sujetaba la pluma de mi gorra cautivó tu atención. ¡Qué hermoso estaría sujetando un velo sobre tu oscura cabellera! Ya ha prendido el de una desposada: mi padre se lo regaló a la que me dio el ser, y ella lo llevó al altar... ¿Lo quieres?\n-No sé en el tuyo -contestó la hermosa-, pero en mi país, una prenda recibida compromete la voluntad. Sólo en un día de ceremonia debe aceptarse un presente de manos de un deudo..., que aún puede ir a Roma sin volver con las manos vacías.\nEl acento helado con que Beatriz pronunció estas palabras turbó un momento al joven, que después de serenarse dijo con tristeza:\n-Lo sé prima; pero hoy se celebran Todos los Santos, y el tuyo entre todos; hoy es día de ceremonias y presentes. ¿Quieres aceptar el mío?\nBeatriz se mordió ligeramente los labios y extendió la mano para tomar la joya, sin añadir una palabra.\nLos dos jóvenes volvieron a quedarse en silencio, y volviose a oír la cascada voz de las viejas que hablaban de brujas y de trasgos, y el zumbido del aire que hacía crujir los vidrios de las ojivas, y el triste y monótono doblar de las campanas.\nAl cabo de algunos minutos, el interrumpido diálogo tornó a anudarse de este modo:\n-Y antes de que concluya el día de Todos los Santos, en que así como el tuyo se celebra el mío, y puedes, sin atar tu voluntad, dejarme un recuerdo, ¿no lo harás? -dijo él, clavando una mirada en la de su prima, que brilló como un relámpago, iluminada por un pensamiento diabólico.\n-¿Por qué no? -exclamó ésta, llevándose la mano al hombro derecho como para buscar alguna cosa entre los pliegues de su ancha manga de terciopelo bordado de oro... Después, con una infantil expresión de sentimiento, añadió:\n-¿Te acuerdas de la banda azul que llevé hoy a la cacería, y que por no sé qué emblema de su color me dijiste que era la divisa de tu alma?\n-Sí.\n-Pues... ¡se ha perdido! Se ha perdido, y pensaba dejártela como un recuerdo.\n-¡Se ha perdido! ¿Y dónde? -preguntó Alonso, incorporándose de su asiento y con una indescriptible expresión de temor y esperanza.\n-No sé...; en el monte acaso.\n-¡En el Monte de las Ánimas -murmuró palideciendo y dejándose caer sobre el sitial-, ¡en el Monte de las Ánimas!\nLuego prosiguió con voz entrecortada y sorda:\n-Tú lo sabes, porque lo habrás oído mil veces; en la ciudad, en toda Castilla me llaman el rey de los cazadores. No habiendo aún podido probar mis fuerzas en los combates, como mis ascendientes, he llevado a esta diversión imagen de la guerra todos los bríos de mi juventud, todo el ardor hereditario en mi raza. La alfombra que pisan tus pies son despojos de fieras que he muerto por mi mano. Yo conozco sus guaridas y sus costumbres; y he combatido con ellas de día y de noche, a pie y a caballo, solo y en batida, y nadie dirá que me ha visto huir el peligro en ninguna ocasión. Otra noche volaría por esa banda, y volaría gozoso como a una fiesta; esta noche..., esta noche, ¿a qué ocultarlo?, tengo miedo. ¿Oyes? Las campanas doblan, la oración ha sonado en San Juan del Duero, las ánimas del monte comenzarán ahora a levantar sus amarillentos cráneos de entre las malezas que cubren sus fosas...; ¡las ánimas!, cuya sola vista puede helar de horror la sangre del más valiente, tornar sus cabellos blancos o arrebatarle en el torbellino de su fantástica carrera como una hoja que arrastra el viento, sin que se sepa adónde.\nMientras el joven hablaba, una sonrisa imperceptible se dibujó en los labios de Beatriz, que cuando hubo concluido exclamó, con un tono indiferente y mientras atizaba el fuego del hogar, donde saltaba y crujía la leña arrojando chispas de mil colores:\n-¡Oh! Eso de ningún modo. ¡Qué locura! ¡Ir ahora al monte por semejante friolera! ¡Una noche tan oscura, noche de Difuntos, y cuajado el camino de lobos!\nAl decir esta última frase, la recargó de un modo tan especial, que Alonso no pudo menos de comprender toda su amarga ironía; movido como por un resorte, se puso de pie, se pasó la mano por la frente, como para arrancarse el miedo que estaba en su cabeza, y no en su corazón, y con voz firme exclamó, dirigiéndose a la hermosa, que estaba aún inclinada sobre el hogar entreteniéndose en revolver el fuego:\n-¡Adiós Beatriz, adiós! Hasta... pronto.\n-¡Alonso, Alonso! -dijo ésta, volviéndose con rapidez; pero cuando quiso, o aparentó querer, detenerle, el joven había desaparecido.\nA los pocos minutos se oyó el rumor de un caballo que se alejaba al galope. La hermosa, con una radiante expresión de orgullo satisfecho, que coloreó sus mejillas, prestó atento oído a aquel rumor, que se debilitaba, que se perdía, que se desvaneció por último.\nLas viejas, en tanto, continuaban en sus cuentos de ánimas aparecidas; el aire zumbaba en los vidrios del balcón, y las campanas de la ciudad doblaban a lo lejos.\n- III -\nHabía pasado una hora, dos, tres; la media noche estaba a punto de sonar, y Beatriz se retiró a su oratorio. Alonso no volvía, no volvía, cuando en menos de una hora pudiera haberlo hecho.\n-¡Habrá tenido miedo! -exclamó la joven cerrando su libro de oraciones y encaminándose a su lecho, después de haber intentado inútilmente murmurar algunos de los rezos que la iglesia consagra en el día de Difuntos a los que ya no existen.\nDespués de haber apagado la lámpara y cruzado las dobles cortinas de seda, se durmió; se durmió con un sueño inquieto, ligero, nervioso.\nLas doce sonaron en el reloj del Postigo. Beatriz oyó entre sueños las vibraciones de la campana, lentas, sordas, tristísimas, y entreabrió los ojos. Creía haber oído, a par de ellas, pronunciar su nombre; pero lejos, muy lejos, y por una voz apagada y doliente. El viento gemía en los vidrios de la ventana.\n-Será el viento -dijo; y poniéndose la mano sobre el corazón procuró tranquilizarse. Pero su corazón latía cada vez con más violencia. Las puertas de alerce del oratorio habían crujido sobre sus goznes, con un chirrido agudo prolongado y estridente.\nPrimero unas y luego las otras más cercanas, todas las puertas que daban paso a su habitación iban sonando por su orden; éstas con un ruido sordo y suave; aquéllas con un lamento largo y crispador. Después, silencio; un silencio lleno de rumores extraños, el silencio de la media noche, con un murmullo monótono de agua distante; lejanos ladridos de perros, voces confusas, palabras ininteligibles; ecos de pasos que van y vienen, crujir de ropas que se arrastran, suspiros que se ahogan, respiraciones fatigosas que casi no se sienten, estremecimientos involuntarios que anuncian la presencia de algo que no se ve y cuya aproximación se nota, no obstante, en la oscuridad.\nBeatriz, inmóvil, temblorosa, adelantó la cabeza fuera de las cortinillas y escuchó un momento. Oía mil ruidos diversos; se pasaba la mano por la frente, tornaba a escuchar; nada, silencio.\nVeía, con esa fosforescencia de la pupila en las crisis nerviosas, como bultos que se movían en todas direcciones; y cuando, dilatándose, las fijaba en un punto, nada; oscuridad, las sombras impenetrables.\n-¡Bah! -exclamó, yendo a recostar su hermosa cabeza sobre la almohada, de raso azul, del lecho-. ¿Soy yo tan miedosa como estas pobres gentes, cuyo corazón palpita de terror bajo una armadura, al oír una conseja de aparecidos?\nY cerrando los ojos intentó dormir...; pero en vano había hecho un esfuerzo sobre sí misma. Pronto volvió a incorporarse, más pálida, más inquieta, más aterrada. Ya no era una ilusión: las colgaduras de brocado de la puerta habían rozado al separarse y unas pisadas lentas sonaban sobre la alfombra; el rumor de aquellas pisadas era sordo, casi imperceptible, pero continuado, y a su compás se oía crujir una cosa como madera o hueso. Y se acercaban, se acercaban, y se movió el reclinatorio que estaba a la orilla de su lecho. Beatriz lanzó un grito agudo, y arrebujándose en la ropa que la cubría escondió la cabeza y contuvo el aliento.\nEl aire azotaba los vidrios del balcón; el agua de la fuente lejana caía y caía con un rumor eterno y monótono; los ladridos de los perros se dilataban en las ráfagas del aire, y las campanas de la ciudad de Soria, unas cerca, otras distantes, doblaban tristemente por las ánimas de los difuntos.\nAsí pasó una hora, dos, la noche, un siglo, porque la noche aquella pareció eterna a Beatriz. Al fin despuntó la aurora; vuelta de su temor, entreabrió los ojos a los primeros rayos de la luz. Después de una noche de insomnio y de terrores, ¡es tan hermosa la luz clara y blanca del día! Separó las cortinas de seda del lecho, y ya se disponía a reírse de sus temores pasados cuando de repente un sudor frío cubrió su cuerpo, sus ojos se desencajaron y una palidez mortal decoloró sus mejillas: sobre el reclinatorio había visto, sangrienta y desgarrada, la banda azul que perdiera en el monte, la banda azul que fue a buscar Alonso.\nCuando sus servidores llegaron despavoridos a noticiarle la muerte del primogénito de Alcudiel, que a la mañana había aparecido devorado por los lobos entre las malezas del Monte de las Ánimas, la encontraron inmóvil, crispada, asida con ambas manos a una de las columnas de ébano del lecho, desencajados los ojos, entreabierta la boca, blancos los labios, rígidos los miembros: muerta, ¡muerta de horror!\n- IV -\nDicen que después de acaecido este suceso un cazador extraviado que pasó la noche de difuntos sin poder salir del Monte de las Ánimas y que al otro día, antes de morir, pudo contar lo que viera, refirió cosas horribles. Entre otras, asegura que vio a los esqueletos de los antiguos Templarios y de los nobles de Soria enterrados en el atrio de la capilla, levantarse al punto de la oración con un estrépito horrible, y caballeros sobre osamentas de corceles perseguir como a una fiera a una mujer hermosa, pálida y desmelenada que, con los pies desnudos y sangrientos y arrojando gritos de horror, daba vueltas alrededor de la tumba de Alonso.", "El Monte de las Ánimas" },
                    { new Guid("c0013a01-dd66-48f0-833b-a68accc35327"), new Guid("3401faeb-e34e-492b-af07-37dc9b6e1bb1"), new DateTime(2024, 12, 1, 12, 0, 21, 0, DateTimeKind.Unspecified), "Relato fantástico sobre las relaciones de pareja.", "Cada vez que yo le hacía observaciones a mi amigo Sabino Ruilópez acerca de su próximo matrimonio, me oía tratar de romántico, de fantástico y hasta de necio.\n—Pero, criatura —me decía, protegiéndome, pues tenía dos años más que yo—, ¿pensarás que no comprendo por qué sientes ese recelo contra mi novia? Son las espinas, las dichosas espinas. ¡Bah! Yo miro las cosas equilibradamente, y no veo en esas espinas el menor obstáculo para la felicidad conyugal.\nLa novia era hija de otro Ruilópez, primo hermano del padre del novio; por tanto, prima segunda de su futuro, lo cual había facilitado las relaciones. Nació la niña un día de Semana Santa, y la madre quiso que se le pusiese de nombre María del Martirio, y se empeñó en que traía, alrededor de la sien, una corona de espinas. Preguntado el médico, declaró que no bahía tal corona, y que sólo se observaban en la frentecita de la recién nacida, entre la pelusa que cubría su cráneo, unas manchas rosa, como huellas de picadas de alfileres. No se necesitó más para acreditar la leyenda. Al morir, moco después, su madre, se hicieron tristes vaticinios respecto a la niña; o moriría también, o su destino sería el convento.\nSe crió, no obstante, normalmente, aunque un poco reconcentrada de carácter y enemiga de bullicio y diversiones. Apenas tuvo amigas, y como sólo vio a su primo, fue natural que la idea de ser su esposa germinase en su espíritu, casi sin preparación. Sabino se empeñó en llevarme a la casa del Martirio, no comprendiendo yo, al pronto, la razón de tal empeño. Luego él mismo acabó por confesarme que se aburría un poco en aquella vivienda melancólica. Después de casado, sería otra cosa, ya se las arreglaría él para transformar a Martirio. Hablaba de Martirio como de algo que te pertenecía, y reía fatuamente, seguro de apoderarse de los últimos resortes secretos de su voluntad.\nEn concepto, pues, de Cirineo del aburrimiento de Sabino, frecuenté el trato de la misteriosa niña. Me atrajo su cara ovalada, como de Virgen de marfil, y, sobre todo, su frente, donde buscaba, sin poderlo evitar, la corona de espinas. Claro es que no podía verla, porque no estaba; pero las manchas delatoras del tormento, allí aparecían bien claras, sobre todo en ciertos días y ocasiones. Y si existían las manchas, ¿no sería que las espinas, invisibles, se hincasen en la piel? La afirmación me parecía concluyente. Resaltaban las huellas de un aro de pinchos en torno de la cabeza virginal. Si Martirio me permitiese apartar con los dedos las ligeras ondulaciones de una cabellera negra y lujosa, sobrado pesada para lo frágil del cuello que sostenía la cabeza, de seguro vería yo continuarse el circulo todo alrededor.\nNo sabré decir lo que había llegado a preocuparme la cuestión de las espinas. Era ya mi idea fija, aunque ocultaba a todos, y en particular a Sabina, mi obsesión. Pero Sabino era un tanto malicioso, y notó mis silencios y mis ojeadas de soslayo a la frente de Martirio.\n—Mira, ten entendido que no pienso hacerte caso, y que tan pronto me licencie, que sólo me faltan dos mesecillos, iré al altar. Con tus fantasías sentimentales sobre las dichosas espinas, me has obligado a consultar a mi médico, y no sabes qué explicación tan natural. Esas señales proceden de la imaginación de la madre. Me ha citado casos muy curiosos y me ha enseñado láminas de obras de medicina. Mi tía (según dice mi tío) meditaba mucho sobre la Pasión. Nada tendría de extraño que, fijando tanto su atención en ciertos pormenores, como el del suplicio de la corona de espinas, la impresión se reflejase en la criatura que llevaba dentro.\n»Bueno; la cosa no tiene la menor importancia. No por eso voy a renunciar a Martirio, que reúne muchas circunstancias para mí. Es muy bonita, es buena, de familia no puedo ponerle tacha alguna, porque es la mía propia, y además, y esto no es de despreciar, aunque los románticos finjan que no importa, tiene ya en la mano una fortuna, la de su madre, y si mi tío no se casa, ¡ya ves, casarse mi tío!, tendrá otra con el tiempo... ¡Las espinas! En cada una pondré un beso, y las borraré. No repliqué nada. Sentí una indignación profunda contra el prosaico criterio.\n»Al volver a mi casa encontré una invitación para un té en casa de la viuda de Valonga. No suelo concurrir a muchos tes; pero un instinto me decidió a aceptar éste. El corazón me brincó al ver que estaba allí Martirio, a quien Sabino hablaba con festiva animación. Él me saludó con sonrisilla irónica, y yo le contesté como distraído. Me alejé, y en un gabinete contiguo, donde no había nadie, me puse a admirar unos cuadros de ninfas y sátiros, en paisajes frescos y densos, a lo Rubens. Con el rabo del ojo observaba a Sabino. Vi que, después de breve y cordial discusión con su novia, se levantaba y se dirigía hacía el comedor, donde la gente se agolpaba ya, Martirio se quedó sola. Su respiración parecía algo fatigosa, y se abanicaba precipitadamente.\n»Sin pensar en lo que hacía, me desembosqué y me senté a su lado.\n—¿No quiere usted tomar nada? —pregunté, con cariño en la voz.\n—No tengo ganas —respondió débilmente—. Sabino comerá por mí y por él...\n—Pero, ¿es que no se siente usted bien? —insistí.\nY al preguntar me fijé, por centésima vez, en las huellas, que me parecieron más abultadas y rubicundas que de costumbre.\n—Sí, no sé lo que tengo hoy —murmuró, con un viso de repentina palidez, más intensa que de costumbre—. Si no fuese porque papá me instó, no vengo.\n—Pasemos a esa otra habitación —le contesté—. Hará menos calor que aquí.\nEra el gabinete de los cuadros estilo Rubens, donde, efectivamente, no había un alma, y el aire era más puro. Nos refugiamos en un sofá vestido de damasco carmesí, y la rodeé de almohadones. Vi que cerraba los ojos, desvaneciéndose, y se me ocurrió ir a buscar agua fresca. Después no me atreví. Iban a alarmarse, a escandalizarse. Por otra parte, no hay cosa más difícil que obtener un vaso de agua en un buffet lleno de gente, y cuando la trajese, de nada serviría ya. Tomé el propio abanico de Martirio y le di aire con toda mi fuerza. Exhaló un suspiro hondo, alzó un poco la cabeza, y luego la dejó rodar sobre mi hombro. Vi que estaba privada de sentido. Volví a abanicarla, llamándola a media voz:\n—¡Martirio, Martirio!.\nY entonces observé que una de las señales de las espinas se abultaba, se hinchaba rápidamente. Era como una ampollita que crece, que adquiere forma esférica. De súbito, abrióse lo mismo que una rosa de Jericó sumergida en agua, y de su seno surgió y resbaló, sobre la marfileña mejilla, una lágrima espesa. Era de sangre, fuerte, fluyente, viva. No sé lo que pasó por mí. Percibí el choque repentino de las grandes revelaciones. Vi claro en mí mismo. Murmurando dulzuras, con los labios recogí la gota de sangre. Mientras la paladeaba ávidamente, otras dos corrieron de la frente torturada. Martirio volvía en sí. Y en vez de fulminarme con su enojo, balbuceaba temblante:\n—¡Qué bien me siento ahora!\nPermanecimos inmóviles, extasiados. Y fue el momento en que se presentó Sabino. Traía en la mano un plato con emparedados para su novia, y era imposible estar en ridículo más completo. De la sorpresa, se le cayó el plato y se hizo añicos. Recobrado ya, se encaró conmigo, amenazador; yo me puse delante de Martirio, escudándola. Casi instantáneamente los ojos del furioso se dilataron, su boca se redondeó, como la boca mecánica de un muñeco. Había visto, en la faz de su prometida, los rastros de sangre, y en mi rasurado mentón un hilo rojo.\nY, exhalando algo que ni era gruñido ni grito, que participaba de ambas cosas, salió corriendo. Enjugué con mi pañuelo el rostro de Martirio, el helado sudor que lo bañaba. Fui a avisar a su padre. Se la llevaron, casi inerte.\nLa ciencia dictaminó. Se trataba de un fenómeno natural, aunque bien raro. Alteraciones circulatorias. Una sugestión imaginativa las provocaba, y en la Edad Media se calificaba de milagro el suceso. Martirio se encerró en su cuarto, sin querer salir de él. Me presenté a su padre. Referí el suceso del baile con toda verdad; ofrecí cuantas repara­ciones considerase precisas. El pobre señor movía la cabeza desconsolado:\n—Tiempo perdido, amigo, y caballerosidad inútil. Mi hija, aunque se lo jure en cruz el protomedicato, no reconoce que lo de las espinas pueda explicarse con términos técnicos. Afirma que es algo sobrenatural que la obliga a consagrarse a Dios para toda su vida. Y, mire usted —agregó, bajando el tono—, es el caso que yo creo que maldita la vocación que mi hija tiene. ¿No piensa usted lo mismo?\nSuspiré, y articulé en voz más honda aún:\n—Estoy con usted.", "Las espinas" }
                });

            migrationBuilder.InsertData(
                table: "StoryTag",
                columns: new[] { "StoryId", "TagId" },
                values: new object[,]
                {
                    { new Guid("728070cf-8796-4e71-bda7-aaae404278bf"), new Guid("43559433-ea81-43c5-8501-f334575fc00c") },
                    { new Guid("728070cf-8796-4e71-bda7-aaae404278bf"), new Guid("fd4cf16b-8c77-4371-a0d3-0bcf0a70d031") },
                    { new Guid("c0013a01-dd66-48f0-833b-a68accc35327"), new Guid("fd4cf16b-8c77-4371-a0d3-0bcf0a70d031") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BookMarks_StoryId",
                table: "BookMarks",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BookMarks_UserId",
                table: "BookMarks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AuthorId",
                table: "Reviews",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_AuthorId",
                table: "Stories",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryTag_TagId",
                table: "StoryTag",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BookMarks");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "StoryTag");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Stories");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
