﻿using Nodex;
using SFML.Graphics;

namespace Callisto;

class AvatarDisplayer : Node
{
    // Fields

    public int ContactIndex = -1;
    public bool IsClickable = false;
    public string ImagePath = "";

    // Public

    public override void Start()
    {
        AddChild(new CircleSprite
        {
            Radius = 100,
            Origin = new(100, 100),
            Texture = GetTexture(),
            OnUpdate = (sprite) =>
            {
                sprite.Position = new(Window.Size.X / 2, 128);
            }
        });

        if (IsClickable)
        {
            CreateNewButton();

            if (ContactIndex != -1)
            {
                if (ContactsContainer.Instance.Contacts[ContactIndex].HasAvatar)
                {
                    CreateDeleteButton();
                }
            }
        }
    }

    // Private

    private Texture GetTexture()
    {
        Texture texture = TextureLoader.Instance.Textures["Avatar"];

        if (ContactIndex != -1)
        {
            Contact contact = ContactsContainer.Instance.Contacts[ContactIndex];

            if (contact.HasAvatar)
            {
                texture = TextureLoader.Instance.Textures[contact.Id.ToString()];
            }
        }

        return texture;
    }

    private void CreateNewButton()
    {
        AddChild(new HalfCircleButton
        {
            Text = "New",
            Style = new()
            {
                TextColor = new(255, 255, 255, 255),
                FillColor = new(0, 0, 0, 0),
                HoverFillColor = new(0, 96, 0, 128),
                PressedFillColor = new(0, 48, 0, 196),
                UnpressedFillColor = new(0, 0, 0, 0)
            },
            OnUpdate = (button) =>
            {
                button.Visible = button.Style.FillColor != button.Style.UnpressedFillColor;
                button.Position = new(Window.Size.X / 2, Window.Size.Y * 0.2F);
            },
            OnClick = OpenPhotoSelectionDialog
        });
    }

    private void CreateDeleteButton()
    {
        AddChild(new BottomHalfCircleButton
        {
            Text = "Delete",
            Origin = new(100, 0),
            Style = new()
            {
                TextColor = new(255, 255, 255, 255),
                FillColor = new(0, 0, 0, 0),
                HoverFillColor = new(96, 0, 0, 128),
                PressedFillColor = new(96, 0, 0, 196),
                UnpressedFillColor = new(0, 0, 0, 0)
            },
            OnUpdate = (button) =>
            {
                button.Visible = button.Style.FillColor != button.Style.UnpressedFillColor;
                button.Position = new(Window.Size.X / 2, Window.Size.Y * 0.2F);
            },
            OnClick = DeleteAvatar
        });
    }

    // Callbacks

    private void OpenPhotoSelectionDialog()
    {
        OpenFileDialog openFileDialog = new();
        openFileDialog.ShowDialog();

        string imagePath = openFileDialog.FileName;

        if (imagePath != null)
        {
            string extension = Path.GetExtension(imagePath);

            if (extension == ".jpg" || extension == ".png")
            {
                ImagePath = imagePath;
                GetChild<CircleSprite>("CircleSprite").Texture = new(imagePath);
                CreateDeleteButton();
            }
        }
    }

    private void DeleteAvatar()
    {
        ImagePath = "";
        GetChild<CircleSprite>("CircleSprite").Texture = TextureLoader.Instance.Textures["Avatar"];
        GetChild<BottomHalfCircleButton>().Destroy();
    }
}