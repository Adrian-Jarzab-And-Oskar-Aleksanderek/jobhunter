namespace Scraper;

internal class Program 
{
    public static void Main(string[] args) {
       Scraper.Scrap("https://www.pracuj.pl/praca/data-warehouse-analyst-warszawa,oferta,1003658385?sug=oferta_bottom_bd_1_tname_214_tgroup_A_promoted", "div[data-test=\"text-earningAmount\"]");
    }
}