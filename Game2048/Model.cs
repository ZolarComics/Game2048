using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Game2048
{
    class Model
    {
        private static SolidColorBrush StaleFor2 = new SolidColorBrush();
        private static SolidColorBrush StaleFor4 = new SolidColorBrush();
        private static SolidColorBrush StaleFor8 = new SolidColorBrush();
        private static SolidColorBrush StaleFor16 = new SolidColorBrush();
        private static SolidColorBrush StaleFor32 = new SolidColorBrush();
        private static SolidColorBrush StaleFor64 = new SolidColorBrush();
        private static SolidColorBrush StaleFor128 = new SolidColorBrush();
        private static SolidColorBrush StaleFor256 = new SolidColorBrush();
        private static SolidColorBrush StaleFor512 = new SolidColorBrush();
        private static SolidColorBrush StaleFor1024 = new SolidColorBrush();
        private static SolidColorBrush StaleFor2048 = new SolidColorBrush();

        private static SolidColorBrush Empty = new SolidColorBrush();
    }
}
