

namespace DAL.Entities
{
    public enum LotStatus
    {
        Created = 1,
        Sold_Out = 2,
        Traded = 3,
        Not_sold = 4
    }

    public enum LotDetailStatus
    {
        Bid_placed = 1,
        Sold_On = 2,
        Interrupted_bit = 3,
        Trade_ended = 4
    }
}
