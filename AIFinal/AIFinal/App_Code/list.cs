using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIFinal.App_Code
{
    public class list
    {
        private List<Node> n;

        public list()
        {
            n = new List<Node>();
        }

        public void add(Node n)
        {
            this.n.Add(n);
        }

        public Node DoHillClimb(Node n)
        {
            int postionHill = 0;
            postionHill = this.n.FindIndex(r => r.MatID == n.MatID);
            // check node both sides
            if ( postionHill < this.n.Count && postionHill > 0)
            {
                double nextWeight = this.n.ElementAt(postionHill + 1).mdgen.weightedGame + this.n.ElementAt(postionHill + 1).mdgen.weightedEmote;
                double previouWeight = this.n.ElementAt(postionHill -1).mdgen.weightedGame + this.n.ElementAt(postionHill -1).mdgen.weightedEmote;
                double currentWeight = this.n.ElementAt(postionHill ).mdgen.weightedGame + this.n.ElementAt(postionHill ).mdgen.weightedEmote;
                double[] weights= { nextWeight, previouWeight, currentWeight};
                double biggestNumber = weights.Max();
                if (biggestNumber == nextWeight)
                {
                    return this.n.ElementAt(postionHill +1 );
                }
                else
                {
                    if  (biggestNumber == previouWeight)
                    {
                        return this.n.ElementAt(postionHill - 1);
                    }
                    else
                    {
                        return this.n.ElementAt(postionHill);
                    }
                }   
            }
            // check only previous
            if (postionHill == this.n.Count)
            {
                double previouWeight = this.n.ElementAt(postionHill - 1).mdgen.weightedGame + this.n.ElementAt(postionHill - 1).mdgen.weightedEmote;
                double currentWeight = this.n.ElementAt(postionHill).mdgen.weightedGame + this.n.ElementAt(postionHill).mdgen.weightedEmote;
                double[] weights = { previouWeight, currentWeight };
                double biggestNumber = weights.Max();
                if (biggestNumber == currentWeight)
                {
                    return this.n.ElementAt(postionHill + 1);
                }
                else
                {
                    if (biggestNumber == previouWeight)
                    {
                        return this.n.ElementAt(postionHill - 1);
                    }
                    else
                    {
                        return this.n.ElementAt(postionHill);
                    }
                }
            }
            //check only next
            if (postionHill == 0)
            {
                double nextWeight = this.n.ElementAt(postionHill + 1).mdgen.weightedGame + this.n.ElementAt(postionHill + 1).mdgen.weightedEmote;
                double currentWeight = this.n.ElementAt(postionHill).mdgen.weightedGame + this.n.ElementAt(postionHill).mdgen.weightedEmote;
                double[] weights = { nextWeight, currentWeight };
                double biggestNumber = weights.Max();
                if (biggestNumber == currentWeight)
                {
                    return this.n.ElementAt(postionHill + 1);
                }
                else
                {
                    return this.n.ElementAt(postionHill);
                }
            }
            else
            {
                return n;
            }

        }
    }
}