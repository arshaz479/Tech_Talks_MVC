namespace Tech_Talks_MVC.Models
{//Reperesents a discussion
    public class Discussion
    {
        //Discussion id 
        public int Id { get; set; }

        //Discussion topic
        public string Topic { get; set; }

        //Addional information
        public string Description { get; set; }
    }
}
