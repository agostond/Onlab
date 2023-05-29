using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{

    /*
     *  @brief This class makes a record useable in a Listbox.
     *  
     *  @note You must add a nem to a record and also an ID.
     *  
     *  @note This class won't store the real password and username, this only helps identify a record in a textbox. 
     *  
     *  @param ID: It's need to identify the record.
     *  
     *  @param PageName: This name will be shown in the text box.
     *
     */
    public class Password
    {
        public string PageName { get; private set; }
        public uint Id { get; private set; }

        public Password(string pageName, uint id)
        {
            if (pageName == null) throw new ArgumentNullException("You must add a name to the password!");
            PageName = pageName;
            Id = id;
        }

        public override string ToString()
        {
            return PageName;
        }

    }
}
