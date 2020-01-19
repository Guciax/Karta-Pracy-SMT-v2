using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Graffiti.MST.ComponentsTools;

namespace Karta_Pracy_SMT_v2
{
    public class ComponentsOnSmtLineLocation
    {

        private static List<ComponentStruct> allComponents { get; set; }

        public static IEnumerable<ComponentStruct> thisLineOtherComponents
        {
            get
            {
                return allComponents.Where(c => c.componentType != ComponentType.LedDiode
                                                      & c.componentType != ComponentType.PCB);
            }
        }
        public static IEnumerable<ComponentStruct> thisLineLedDiodes
        {
            get
            {
                return allComponents.Where(c => c.componentType == ComponentType.LedDiode);
            }
        }
        public static IEnumerable<ComponentStruct> thisLineLedPcbs
        {
            get
            {
                return allComponents.Where(c => c.componentType == ComponentType.PCB);
            }
        }

        public static void Reload()
        {
            var locations = Graffiti.MST.ComponentsTools.GetDbData.GetComponentsInLocations(Graffiti.MST.ComponentsLocations.LineNumberToLocation(DataStorage.GlobalParameters.SmtLine)).SelectMany(x=>x.Value).ToList();
            allComponents = Graffiti.MST.ComponentsTools.GetDbData.GetComponentDataWithAttributes(locations).ToList();
        }
    }
}
