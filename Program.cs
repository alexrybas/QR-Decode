using System;
using System.Collections.Generic;
using System.Linq;

public class CodeWars
{
    public static string Scanner(int[][] qrcode)
    {
        byte ByteBitsToByte(IEnumerable<byte> bytes) => (byte)bytes.Select((val, i) => val << (7 - i)).Sum();

        var demaskedBits = CalcGridOrder().Select(o => (o.x + o.y) % 2 == 0 ? (byte)(qrcode[o.y][o.x] ^ 1) : (byte)qrcode[o.y][o.x]);

        var len = ByteBitsToByte(demaskedBits.Skip(4).Take(8));

        return new String(demaskedBits.Skip(12).Chunk(8).Select(b => (char)ByteBitsToByte(b)).Take(len).ToArray());
    }

    static List<(int x, int y)> CalcGridOrder()
    {
        List<(int, int)> order = new List<(int, int)>();

        for (int x = 20, y = 20, yDir = -1; x > 12; x -= 2)
        {
            while (y > 8 && y < 21)
            {
                order.Add((x, y));
                order.Add((x - 1, y));
                y += yDir;
            }
            yDir *= -1;
            y += yDir;
        }

        return order;
    }
}