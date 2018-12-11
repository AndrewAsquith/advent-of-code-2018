using System;

namespace day03
{
    class FabricClaim
    {
        public int ClaimId { get; set; }

        public int StartX { get; set; }

        public int EndX
        {
            get
            {
                return StartX + Width;
            }
        }

        public int StartY { get; set; }

        public int EndY
        {
            get
            {
                return StartY + Height;
            }
        }


        public int Width { get; set; }
        public int Height { get; set; }


        public FabricClaim(int id, int x, int y, int w, int h)
        {
            ClaimId = id;
            StartX = x;
            StartY = y;
            Width = w;
            Height = h;
        }

    }
}