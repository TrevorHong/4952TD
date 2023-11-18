using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;

namespace TD.data
{
    public class TowerPlace
    {  
        double xPos = 0;
        double yPos = 0;

        double mouseXPos = 0;
        double mouseYPos = 0;
        public bool towerGenerated { get; set;}
        bool trackMouse = false;
        public void GenerateTower() {
            xPos = mouseXPos;
            yPos = mouseYPos;
            trackMouse = true;
            towerGenerated = true;
            Console.WriteLine("works");
        }

        void PlaceTower() {
            trackMouse = false;
        }

        void UpdatePos(MouseEventArgs e) {
            if(trackMouse) {
                mouseXPos = e.ClientX;
                mouseYPos = e.ClientY;

            }
        }
    }
}