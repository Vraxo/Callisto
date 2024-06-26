﻿using Callisto;
using SFML.Window;

namespace Callisto.ContactInfoViewerNode;

class CopyNumberButton : Button
{
    // Fields

    public int ContactIndex = -1;
    public int NumberIndex = 0;

    // Public

    public override void Start()
    {
        base.Start();

        Position = new(0, Window.Size.Y * 0.55F);
        Size = new(270, 50);
        Style.OutlineThickness = 0;
        Style.FontSize = 24;
        OnClick = CopyPhoneNumber;
    }

    // Callback

    private void CopyPhoneNumber()
    {
        Clipboard.Contents = ContactsContainer.Instance.Contacts[ContactIndex].PhoneNumbers[NumberIndex];
    }
}