﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIFinal.App_Code
{
    public class ReflexAgent
    {
        public ReflexAgent(string gameID, Emotion[] emotionArray, GameEvents ge, string emoteLocation)
        {
            // create reflex agent
            //Percepts
            bool winner = (ge.p.isRadiant == ge.p.radiant_win);

            int totalkillsFriendly = 1;
            int totalKillsEnemy = 1; // base of 1 to reduce changes of undefined 
            if (ge.p.isRadiant)
            {
                totalkillsFriendly = ge.md.radiant_score;
                totalKillsEnemy = ge.md.dire_score;
            }
            else
            {
                totalkillsFriendly = ge.md.dire_score;
                totalKillsEnemy = ge.md.radiant_score;
            }

            double gpm = ge.p.gold_per_min;
            double kpm = ge.p.kills_per_min*10;
            double dpmkpm = 0;
            if (kpm == 0)
            {
                if (gpm > kpm) // bad kpm punish
                {
                    dpmkpm = 0;
                }
                else
                {
                    dpmkpm = 0;
                }
            }
            else
            {
                if (gpm > kpm) // good team participation 
                {
                    dpmkpm = 1;
                }
                else
                {
                    dpmkpm = 0;
                }
            }

            double stomp = 0;
            if (totalkillsFriendly + 20 > totalKillsEnemy)
            {
                stomp = 1;
            }
            else
            {
                if (totalkillsFriendly + 10 > totalKillsEnemy)
                {
                    stomp = 0.7;
                }
                else
                {
                    if (totalkillsFriendly == totalKillsEnemy)
                    {
                        stomp = 0;
                    }
                    else
                    {
                        if (totalkillsFriendly +20 < totalKillsEnemy) // comeback reward
                        {
                            stomp = 1;
                        }
                    }
                }

            }

            // read emotion from disk


            //Condtion Rules
            double weightedGame = weightPerSumOver(winner, ge.p.kda, totalkillsFriendly, ge.p.total_gold, ge.p.gold_per_min, ge.p.last_hits,ge.md.duration,ge.p.hero_damage,ge.p.lane_efficiency,ge.p.gold, dpmkpm, ge.p.deaths, stomp);

            // actuator (MetadataGeneration)
        }



        /*the following fucntion is w weighted sum of game Genral performance, weightPerSumOver = weight per sum over game
         * winner is winning the game weight
         * kda = game performance ration
         * totalTeam = all team kills
         * total_gold = all gold
         * goldpermin = gold per minute
         * last_hits = last hits
         * damageOver = all damage
         * lane efficiency = farming skill
         * gold end = unspent gold == oportunity loss
         * gpmkpm = fighting reward
         * death = deaths
         * stomp = how hard was game won
         */

        public double weightPerSumOver(bool winner, int kda, int totalTeam, double total_gold, int goldpermin, double last_hits, int duration ,int damageOver, float lane_efficiency, int goldEnd, double gpmkpm, int death, double stomp)
        {
            double dwinner = 0;
            if (winner)
            {
                dwinner = 2;
            }
            double Ratio1 = dwinner;
            double Ratio2 = kda / totalTeam;
            double Ratio3 = 0; // wasting gold penalty 
            if (goldEnd > goldpermin * 10) 
            {
                Ratio3 = 1;
            }
            else
            {
                Ratio3 = 0;
            }
            
            double Ratio4 = last_hits/(duration);
            decimal dec = new decimal(lane_efficiency);
            double Ratio5 = (double)dec;
            double Ratio6 = (damageOver/100) / (duration*10);
            double Ratio7 = gpmkpm;
            double Ratio8 = 0;
            if (death > 10)
            {
                Ratio8 = 1;
            }
            else
            {
                Ratio8 = 0;
            }
            return (Ratio1 + Ratio2 + Ratio3 + Ratio4 + Ratio5 + Ratio6 + Ratio7 + Ratio8)/8;
        }

        /* this is game performance and emotion weighted
        * with objectives
        * with lh
        * with gg
        * with dh
        */

        public double Positivity(GameEvents.objectives[] objectives, double[] lht, double[] ght, double[] dnt, Emotion emotionArray)
        {
            double weightedWithEmotions = 0;
            foreach (GameEvents.objectives o in objectives)
            {
                int time = o.time;
                double countlht = 0;
                double countdht = 0;
                double countght = 0;
                double weight1 = 0;
                double weight2 = 0;
                double weight3 = 0;
                // get last hit
                foreach (double l in lht)
                {
                    double timelht = countlht * 60;
                    if (timelht > time)
                    {
                        weight1 = l;
                            break;
                    }
                    countlht++;
                }
                // get ght
                foreach (double d in dnt)
                {
                    double timedht = countdht * 60;
                    if (timedht > time)
                    {
                        weight3 = d;
                            break;
                    }
                    countdht++;
                }
                //Get dnt
                foreach (double g in ght)
                {
                    double timeght = countght * 60;
                    if (timeght > time)
                    {
                        weight3 = g;
                            break;
                    }
                    countght++;
                }
                //ratio them

                // give emotionBias

            }

            return weightedWithEmotions;
        }


    }
}