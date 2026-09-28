using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace CharacterGenerator;

public class Function
{
    public object FunctionHandler(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context)
    {
        var query = request.QueryStringParameters ?? new Dictionary<string, string>();

        var name = query.TryGetValue("name", out var n) ? n : "Hero";
        var level = query.TryGetValue("level", out var lv) && int.TryParse(lv, out var l) ? l : 1;

        var rnd = new Random();
        var weapons = new[] { "Меч", "Посох", "Лук", "Кинжали" };
        var titles = new[] { "Бестрашний", "Тінь ночі", "Мудрий", "Вдачливий" };

        return new
        {
            name,level, hp = 100 + level * 10,
            luck = rnd.Next(1, 101),
            weapon = weapons[rnd.Next(weapons.Length)], 
            title = titles[rnd.Next(titles.Length)]
        };
    }
}