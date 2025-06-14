using System.Text.Json;

namespace Scraper.Models
{
    public class SkillReference
    {
        public SkillReference()
        {
        }

        public SkillReference(string name, string category, List<string> aliases)
        {
            Name = name;
            Category = category;
            Aliases = aliases;
        }

        public string Name { get; set; }
        public string Category { get; set; }
        public List<string> Aliases { get; set; }

        public static List<SkillReference> GetReferenceSkills()
        {
            return
            [
                new SkillReference
                {
                    Name = "Agile", Category = "Agile & Project Management",
                    Aliases =
                    [
                        "Agile Methodology", "Agile Methodologies", "Agile Practice", "Agile Software Development"
                    ]
                },
                new SkillReference
                {
                    Name = "Scrum", Category = "Agile & Project Management",
                    Aliases = ["Scrum Master", "Scrum Methodology"]
                },
                new SkillReference { Name = "Kanban", Category = "Agile & Project Management", Aliases = [] },
                new SkillReference
                {
                    Name = "SAFe", Category = "Agile & Project Management", Aliases = ["Scaled Agile Framework"]
                },
                new SkillReference
                {
                    Name = "Agile Coach", Category = "Agile & Project Management", Aliases = ["Agile Coaching"]
                },
                new SkillReference
                {
                    Name = "Agile Product Management", Category = "Agile & Project Management",
                    Aliases = ["Agile Product Owner"]
                },
                new SkillReference
                {
                    Name = "Agile Release Train", Category = "Agile & Project Management", Aliases = ["ART"]
                },
                new SkillReference
                {
                    Name = "Agile Transformations", Category = "Agile & Project Management",
                    Aliases = ["Agile Transformation"]
                },
                new SkillReference
                {
                    Name = "Agile & Waterfall", Category = "Agile & Project Management", Aliases = ["Waterfall"]
                },
                new SkillReference
                {
                    Name = "Agile Certification", Category = "Agile & Project Management",
                    Aliases = ["Agile Certified Practitioner"]
                },

                // Programming Languages (All)
                new SkillReference
                {
                    Name = "JavaScript", Category = "Programming Languages", Aliases = ["JS", "ECMAScript"]
                },
                new SkillReference { Name = "HTML/CSS", Category = "Programming Languages", Aliases = ["HTML", "CSS"] },
                new SkillReference { Name = "Python", Category = "Programming Languages", Aliases = ["Py"] },
                new SkillReference { Name = "SQL", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "TypeScript", Category = "Programming Languages", Aliases = ["TS"] },
                new SkillReference(){ Name = "Bash/Shell", Category = "Programming Languages", Aliases = ["Shell", "Bash"] },
                new SkillReference { Name = "Java", Category = "Programming Languages", Aliases = [] },
                new SkillReference (){ Name = "C#", Category = "Programming Languages", Aliases = ["CSharp", "C Sharp"] },
                new SkillReference { Name = "C++", Category = "Programming Languages", Aliases = ["CPP"] },
                new SkillReference { Name = "C", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "PHP", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "PowerShell", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Go", Category = "Programming Languages", Aliases = ["Golang"] },
                new SkillReference { Name = "Kotlin", Category = "Programming Languages", Aliases = [] },
                new SkillReference  { Name = "Rust", Category = "Programming Languages", Aliases = []},
                new SkillReference { Name = "Lua", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Dart", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Assembly", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Ruby", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Swift", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "R", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Visual Basic", Category = "Programming Languages", Aliases = ["VB"] },
                new SkillReference { Name = "MATLAB", Category = "Programming Languages", Aliases = [] },
                new SkillReference(){
                    Name = "VBA", Category = "Programming Languages",
                    Aliases = ["Visual Basic for Applications"]
                },
                new SkillReference { Name = "Groovy", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Scala", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Perl", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "GDScript", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Objective-C", Category = "Programming Languages", Aliases = ["ObjC"] },
                new SkillReference { Name = "Elixir", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Haskell", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Delphi", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "MicroPython", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Lisp", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Clojure", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Julia", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Zig", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Fortran", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Solidity", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Ada", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Erlang", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "F#", Category = "Programming Languages", Aliases = ["FSharp"] },
                new SkillReference { Name = "Apex", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Prolog", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "OCaml", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Cobol", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Crystal", Category = "Programming Languages", Aliases = [] },
                new SkillReference { Name = "Nim", Category = "Programming Languages", Aliases = [] },
                new SkillReference(){ Name = "Zephyr", Category = "Programming Languages", Aliases = [] },

                // Databases (All)
                new SkillReference(){ Name = "PostgreSQL", Category = "Databases", Aliases = ["Postgres", "Postgres DB"] },
                new SkillReference(){ Name = "MySQL", Category = "Databases", Aliases = ["MySQL DB"] },
                new SkillReference(){ Name = "SQLite", Category = "Databases", Aliases = [] },
                new SkillReference(){
                    Name = "Microsoft SQL Server", Category = "Databases", Aliases = ["MSSQL", "SQL Server"]
                },
                new SkillReference(){ Name = "MongoDB", Category = "Databases", Aliases = ["Mongo", "Mongo DB"] },
                new SkillReference(){ Name = "Redis", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "MariaDB", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "Elasticsearch", Category = "Databases", Aliases = ["Elastic"] },
                new SkillReference(){ Name = "Oracle", Category = "Databases", Aliases = ["Oracle DB"] },
                new SkillReference(){ Name = "DynamoDB", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "Firebase Realtime Database", Category = "Databases", Aliases = ["Firebase"] },
                new SkillReference(){ Name = "Cloud Firestore", Category = "Databases", Aliases = ["Firestore"] },
                new SkillReference(){ Name = "BigQuery", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "Microsoft Access", Category = "Databases", Aliases = ["Access"] },
                new SkillReference(){ Name = "Supabase", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "H2", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "Cosmos DB", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "Snowflake", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "InfluxDB", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "Cassandra", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "Databricks SQL", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "Neo4J", Category = "Databases", Aliases = ["Neo4j"] },
                new SkillReference(){ Name = "IBM DB2", Category = "Databases", Aliases = ["DB2"] },
                new SkillReference(){ Name = "Clickhouse", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "Solr", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "DuckDB", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "Firebird", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "Couch DB", Category = "Databases", Aliases = ["CouchDB"] },
                new SkillReference(){ Name = "Cockroachdb", Category = "Databases", Aliases = ["Cockroach DB"] },
                new SkillReference(){ Name = "Couchbase", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "Presto", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "Datomic", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "EventStoreDB", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "RavenDB", Category = "Databases", Aliases = [] },
                new SkillReference(){ Name = "TiDB", Category = "Databases", Aliases = [] },

                // Cloud Platforms
                new SkillReference(){
                    Name = "AWS", Category = "Cloud Platforms", Aliases = ["Amazon Web Services", "Amazon AWS"]
                },
                new SkillReference(){ Name = "Azure", Category = "Cloud Platforms", Aliases = ["Microsoft Azure"] },
                new SkillReference(){
                    Name = "GCP", Category = "Cloud Platforms",
                    Aliases = ["Google Cloud", "Google Cloud Platform"]
                },
                new SkillReference(){ Name = "DigitalOcean", Category = "Cloud Platforms", Aliases = ["DO"] },
                new SkillReference(){ Name = "Heroku", Category = "Cloud Platforms", Aliases = [] },
                new SkillReference(){ Name = "Vercel", Category = "Cloud Platforms", Aliases = [] },
                new SkillReference(){ Name = "Netlify", Category = "Cloud Platforms", Aliases = [] },
                new SkillReference(){ Name = "Alibaba Cloud", Category = "Cloud Platforms", Aliases = ["Aliyun"] },
                new SkillReference(){ Name = "IBM Cloud", Category = "Cloud Platforms", Aliases = ["Bluemix"] },

                // Web Frameworks
                new SkillReference(){
                    Name = "React", Category = "Web Frameworks", Aliases = ["ReactJS", "React.js", "React JS"]
                },
                new SkillReference(){
                    Name = "Angular", Category = "Web Frameworks",
                    Aliases = ["AngularJS", "Angular.js", "Angular JS"]
                },
                new SkillReference(){ Name = "Vue", Category = "Web Frameworks", Aliases = ["VueJS", "Vue.js", "Vue JS"] },
                new SkillReference(){ Name = "Next.js", Category = "Web Frameworks", Aliases = ["NextJS", "Next JS"] },
                new SkillReference(){ Name = "Express", Category = "Web Frameworks", Aliases = ["Express.js", "ExpressJS"] },
                new SkillReference(){ Name = "Django", Category = "Web Frameworks", Aliases = [] },
                new SkillReference(){ Name = "Flask", Category = "Web Frameworks", Aliases = [] },
                new SkillReference(){ Name = "Spring", Category = "Web Frameworks", Aliases = ["Spring Boot"] },
                new SkillReference(){ Name = "ASP.NET", Category = "Web Frameworks", Aliases = ["ASP.NET Core", "ASP Core"] },
                new SkillReference(){ Name = "Laravel", Category = "Web Frameworks", Aliases = [] },
                new SkillReference(){ Name = "Ruby on Rails", Category = "Web Frameworks", Aliases = ["Rails", "RoR"] },
                new SkillReference(){ Name = "FastAPI", Category = "Web Frameworks", Aliases = [] },
                new SkillReference(){ Name = "NestJS", Category = "Web Frameworks", Aliases = ["Nest.js", "Nest JS"] },
                new SkillReference(){ Name = "Koa.js", Category = "Web Frameworks", Aliases = ["KoaJS", "Koa JS"] },
                new SkillReference(){ Name = "Phoenix", Category = "Web Frameworks", Aliases = [] },
                new SkillReference(){ Name = "Play Framework", Category = "Web Frameworks", Aliases = ["Play"] },
                new SkillReference(){ Name = "Svelte", Category = "Web Frameworks", Aliases = [] },
                new SkillReference(){ Name = "Nuxt.js", Category = "Web Frameworks", Aliases = ["NuxtJS", "Nuxt JS"] },
                new SkillReference(){ Name = "Gatsby", Category = "Web Frameworks", Aliases = [] },
                new SkillReference(){ Name = "Ember.js", Category = "Web Frameworks", Aliases = ["EmberJS", "Ember JS"] },
                new SkillReference(){
                    Name = "Backbone.js", Category = "Web Frameworks", Aliases = ["BackboneJS", "Backbone JS"]
                },
                new SkillReference(){ Name = "jQuery", Category = "Web Frameworks", Aliases = [] },

                // DevOps
                new SkillReference(){ Name = "Docker", Category = "DevOps", Aliases = ["Docker Container"] },
                new SkillReference(){ Name = "Kubernetes", Category = "DevOps", Aliases = ["K8s", "K8"] },
                new SkillReference(){ Name = "Jenkins", Category = "DevOps", Aliases = ["Jenkins CI", "Jenkins CD"] },
                new SkillReference(){ Name = "Git", Category = "DevOps", Aliases = ["Git Version Control"] },
                new SkillReference(){ Name = "Terraform", Category = "DevOps", Aliases = [] },
                new SkillReference(){ Name = "Ansible", Category = "DevOps", Aliases = [] },
                new SkillReference(){ Name = "Puppet", Category = "DevOps", Aliases = [] },
                new SkillReference(){ Name = "Chef", Category = "DevOps", Aliases = [] },
                new SkillReference(){ Name = "Prometheus", Category = "DevOps", Aliases = [] },
                new SkillReference(){ Name = "Grafana", Category = "DevOps", Aliases = [] },
                new SkillReference(){ Name = "ELK Stack", Category = "DevOps", Aliases = ["Elastic Stack"] },
                new SkillReference(){ Name = "GitLab CI", Category = "DevOps", Aliases = ["GitLab CI/CD"] },
                new SkillReference(){ Name = "GitHub Actions", Category = "DevOps", Aliases = [] },
                new SkillReference(){ Name = "CircleCI", Category = "DevOps", Aliases = [] },
                new SkillReference(){ Name = "Travis CI", Category = "DevOps", Aliases = [] },
                new SkillReference(){ Name = "TeamCity", Category = "DevOps", Aliases = [] },
                new SkillReference(){ Name = "Bamboo", Category = "DevOps", Aliases = [] },

                // Testing
                new SkillReference(){ Name = "Jest", Category = "Testing", Aliases = [] },
                new SkillReference(){ Name = "JUnit", Category = "Testing", Aliases = [] },
                new SkillReference(){ Name = "PyTest", Category = "Testing", Aliases = ["Pytest"] },
                new SkillReference(){ Name = "Selenium", Category = "Testing", Aliases = [] },
                new SkillReference(){ Name = "Cypress", Category = "Testing", Aliases = [] },
                new SkillReference(){ Name = "Mocha", Category = "Testing", Aliases = [] },
                new SkillReference(){ Name = "Chai", Category = "Testing", Aliases = [] },
                new SkillReference(){ Name = "Jasmine", Category = "Testing", Aliases = [] },
                new SkillReference(){ Name = "Karma", Category = "Testing", Aliases = [] },
                new SkillReference(){ Name = "Playwright", Category = "Testing", Aliases = [] },
                new SkillReference(){ Name = "Puppeteer", Category = "Testing", Aliases = [] },
                new SkillReference(){ Name = "TestNG", Category = "Testing", Aliases = [] },
                new SkillReference(){ Name = "Robot Framework", Category = "Testing", Aliases = [] },
                new SkillReference(){ Name = "Cucumber", Category = "Testing", Aliases = [] },
                new SkillReference(){ Name = "SpecFlow", Category = "Testing", Aliases = [] },

                // Tools
                new SkillReference(){ Name = "GitHub", Category = "Tools", Aliases = [] },
                new SkillReference(){ Name = "GitLab", Category = "Tools", Aliases = [] },
                new SkillReference(){ Name = "Jira", Category = "Tools", Aliases = [] },
                new SkillReference(){ Name = "Confluence", Category = "Tools", Aliases = [] },
                new SkillReference(){ Name = "VS Code", Category = "Tools", Aliases = ["Visual Studio Code"] },
                new SkillReference(){ Name = "Visual Studio", Category = "Tools", Aliases = ["VS"] },
                new SkillReference(){ Name = "IntelliJ IDEA", Category = "Tools", Aliases = ["IntelliJ"] },
                new SkillReference(){ Name = "Eclipse", Category = "Tools", Aliases = [] },
                new SkillReference(){ Name = "PyCharm", Category = "Tools", Aliases = [] },
                new SkillReference(){ Name = "WebStorm", Category = "Tools", Aliases = [] },
                new SkillReference(){ Name = "Postman", Category = "Tools", Aliases = [] },
                new SkillReference(){ Name = "Swagger", Category = "Tools", Aliases = ["OpenAPI"] },
                new SkillReference(){ Name = "Figma", Category = "Tools", Aliases = [] },
                new SkillReference(){ Name = "Adobe XD", Category = "Tools", Aliases = ["XD"] },
                new SkillReference(){ Name = "Sketch", Category = "Tools", Aliases = [] },
                new SkillReference(){ Name = "Slack", Category = "Tools", Aliases = [] },
                new SkillReference(){ Name = "Microsoft Teams", Category = "Tools", Aliases = ["Teams"] },
                new SkillReference(){ Name = "Zoom", Category = "Tools", Aliases = [] },
                new SkillReference(){ Name = "Trello", Category = "Tools", Aliases = [] },
                new SkillReference(){ Name = "Asana", Category = "Tools", Aliases = [] },
                new SkillReference(){ Name = "Notion", Category = "Tools", Aliases = [] },

                // Mobile Development
                new SkillReference(){ Name = "React Native", Category = "Mobile Development", Aliases = ["React Native"] },
                new SkillReference(){ Name = "Flutter", Category = "Mobile Development", Aliases = [] },
                new SkillReference(){ Name = "Xamarin", Category = "Mobile Development", Aliases = [] },
                new SkillReference(){ Name = "Ionic", Category = "Mobile Development", Aliases = [] },
                new SkillReference(){ Name = "Cordova", Category = "Mobile Development", Aliases = ["PhoneGap"] },
                new SkillReference(){ Name = "NativeScript", Category = "Mobile Development", Aliases = [] },

                // AI/ML
                new SkillReference(){ Name = "TensorFlow", Category = "AI/ML", Aliases = [] },
                new SkillReference(){ Name = "PyTorch", Category = "AI/ML", Aliases = [] },
                new SkillReference(){ Name = "Keras", Category = "AI/ML", Aliases = [] },
                new SkillReference(){ Name = "scikit-learn", Category = "AI/ML", Aliases = ["sklearn"] },
                new SkillReference(){ Name = "OpenCV", Category = "AI/ML", Aliases = [] },
                new SkillReference(){ Name = "NLTK", Category = "AI/ML", Aliases = [] },
                new SkillReference(){ Name = "SpaCy", Category = "AI/ML", Aliases = [] },
                new SkillReference(){ Name = "Hugging Face", Category = "AI/ML", Aliases = ["Transformers"] },

                // Game Development
                new SkillReference(){ Name = "Unity", Category = "Game Development", Aliases = [] },
                new SkillReference(){ Name = "Unreal Engine", Category = "Game Development", Aliases = ["UE4", "UE5"] },
                new SkillReference(){ Name = "Godot", Category = "Game Development", Aliases = [] },
                new SkillReference(){ Name = "Cocos2d", Category = "Game Development", Aliases = [] },
                new SkillReference(){ Name = "Phaser", Category = "Game Development", Aliases = [] },

                // Blockchain
                new SkillReference(){ Name = "Solidity", Category = "Blockchain", Aliases = [] },
                new SkillReference(){ Name = "Web3.js", Category = "Blockchain", Aliases = ["Web3"] },
                new SkillReference(){ Name = "Ethereum", Category = "Blockchain", Aliases = [] },
                new SkillReference(){ Name = "Hyperledger", Category = "Blockchain", Aliases = [] },
                new SkillReference(){ Name = "Truffle", Category = "Blockchain", Aliases = [] },

                // Security
                new SkillReference(){ Name = "OWASP", Category = "Security", Aliases = [] },
                new SkillReference(){ Name = "Burp Suite", Category = "Security", Aliases = [] },
                new SkillReference(){ Name = "Metasploit", Category = "Security", Aliases = [] },
                new SkillReference(){ Name = "Wireshark", Category = "Security", Aliases = [] },
                new SkillReference(){ Name = "Nmap", Category = "Security", Aliases = [] },
                new SkillReference(){ Name = "Kali Linux", Category = "Security", Aliases = ["Kali"] },

                // SAP
                new SkillReference(){ Name = "SAP", Category = "SAP", Aliases = ["SAP ERP", "SAP R/3", "SAP ECC"] },
                new SkillReference(){ Name = "SAP HANA", Category = "SAP", Aliases = [] },
                new SkillReference(){ Name = "SAP ABAP", Category = "SAP", Aliases = ["ABAP"] }
            ];
        }

        public static void SaveToFile(string filePath = "skill_reference.json")
        {
            var skills = GetReferenceSkills();
            var json = JsonSerializer.Serialize(skills, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public static List<SkillReference> LoadFromFile(string filePath = "skill_reference.json")
        {
            if (!File.Exists(filePath))
                return GetReferenceSkills();

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<SkillReference>>(json) ?? GetReferenceSkills();
        }
    }
} 