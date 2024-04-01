using Nodex;
using Callisto.ContactsListNode;

namespace Callisto;

class EntryPoint
{
    public static void Main(string[] args)
    {
        Program.Start(new ContactsList());
    }
}