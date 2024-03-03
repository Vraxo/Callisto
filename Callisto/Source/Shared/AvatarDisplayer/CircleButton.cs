using Nodex;

namespace Callisto.AvatarDisplayerNode;

class CircleButton : Nodex.CircleButton
{
    // Fields

    public int ContactIndex = -1;

    // Public

    public override void Start()
    {
        base.Start();

        Radius = 100;
        Origin = new(Radius, Radius);
        actionOnClick = OpenPhotoSelectionDialog;
    }

    public override void Update()
    {
        base.Update();

        Position = new(Window.Size.X / 2, Window.Size.Y * 0.2F);
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

            if (extension == ".png" || extension == ".jpg")
            {
                GetNode<CircleSprite>("AvatarDisplayer/CircleSprite").Texture = new(imagePath);
                GetParent<AvatarDisplayer>().ImagePath = imagePath;
            }
        }
    }
}