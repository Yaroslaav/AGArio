using SFML.Graphics;
using SFML.System;

interface IAnimated
{
    Texture texture { get; protected set; }
    Sprite _sprite { get; protected set; }
    Vector2i spriteSize { get; protected set; }
    int currentFrame { get; protected set; }
    int milliSecondsBetweenAnimation { get; protected set; }
    float lastAnimationTime { get; protected set; }

    void UpdateAnimation();
    void TrySetNextFrame();
    void UpdateSprite();
}