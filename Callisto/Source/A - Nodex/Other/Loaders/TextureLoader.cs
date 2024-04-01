﻿using SFML.Graphics;

namespace Nodex;

class TextureLoader
{
    // AllFields

    public Dictionary<string, Texture> Textures = [];

    // Singleton

    private static TextureLoader? instance;

    public static TextureLoader Instance
    {
        get
        {
            instance ??= new();
            return instance;
        }
    }

    private TextureLoader()
    {
        Textures.Add("Avatar", new("Resources/DefaultAvatar.jpg"));
    }
}