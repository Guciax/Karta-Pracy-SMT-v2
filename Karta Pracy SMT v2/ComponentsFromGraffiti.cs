using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Graffiti.MST.ComponentsTools;

namespace Karta_Pracy_SMT_v2
{
    public class ComponentsFromGraffiti
    {
        private static IEnumerable<ComponentStruct> allComponents;
        public static IEnumerable<ComponentStruct> thisLineAllComponents
        {
            get
            {
                return allComponents.Where(c => c.Location == Graffiti.MST.ComponentsLocations.LineNumberToLocation(DataStorage.GlobalParameters.SmtLine));
            }
        }

        public static IEnumerable<ComponentStruct> thisLineOtherComponents
        {
            get
            {
                return thisLineAllComponents.Where(c => c.componentType != ComponentType.LedDiode 
                                                      & c.componentType != ComponentType.PCB);
            }
        }
        public static IEnumerable<ComponentStruct> thisLineLedDiodes
        {
            get
            {
                return thisLineAllComponents.Where(c => c.componentType == ComponentType.LedDiode);
            }
        }
        public static IEnumerable<ComponentStruct> thisLineLedPcbs
        {
            get
            {
                return thisLineAllComponents.Where(c => c.componentType == ComponentType.PCB);
            }
        }

        public static async Task LoadComponentsAsync()
        {
            Dictionary<string, List<string>> locations = new Dictionary<string, List<string>>();

            await new Task(() => locations = Graffiti.MST.ComponentsTools.GetDbData.GetComponentsInLocations("EL2."));
            var componentsList = locations.SelectMany(x => x.Value).Where(x => x.StartsWith("4010460") || x.StartsWith("4010560")).ToList();
            await new Task(() => allComponents = Graffiti.MST.ComponentsTools.GetDbData.GetComponentDataWithAttributes(componentsList));
        }
    }
}
