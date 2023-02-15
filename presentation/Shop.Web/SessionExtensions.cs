using System.Text;
using Shop.Web.Models;

namespace Shop.Web;

public static class SessionExtensions
{
    private const string key = "Card";
    // как я понял сессия хранит свое состояние до запроса и перезаписывается (меняется) каждый запрос
    // и еще. Порядок чтения и записи должен совпадать, иначе в поле key может попасть значние value или amount
    
    public static void Set(this ISession session, Cart card)
    {
        if (card == null)
            return;
        
        using(var stream = new MemoryStream())
        using(var writer = new BinaryWriter(stream,Encoding.UTF8, true))
        {
            writer.Write(card.OrderId);
            writer.Write(card.TotalCount);
            writer.Write(card.TotalPrice);
          
            session.Set(key,stream.ToArray());
        }
    }

    public static bool TryGetCard(this ISession session, out Cart? card)
    {
        if (session.TryGetValue(key, out byte[] buffer))
        {
            using (var stream = new MemoryStream(buffer))
            using (var reader = new BinaryReader(stream,Encoding.UTF8,true))
            {
                var orderId = reader.ReadInt32();
                var totalCount = reader.ReadInt32();
                var totalPrice = reader.ReadDecimal();

                card = new Cart(orderId)
                {
                    TotalCount = totalCount,
                    TotalPrice = totalPrice
                };
                              
                return true;
            }
        }

        card = null;
        return false;
    }
}