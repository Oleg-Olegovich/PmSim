namespace PmSim.Shared.Contracts.Actions
{
    public class EmployeeInfoResponse
    {
        public (int, int) NameNumbers { get; set; }

        public int DesiredSalary { get; set; }

        public int Programming { get; set; }
        
        public int Music { get; set; }
        
        public int Design { get; set; }
        
        public int Management { get; set; }
        
        public int Creativity { get; set; }
    }
}