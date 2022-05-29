#version 330 core

uniform vec2 viewPortSize;

layout (location = 0) in vec2 aPosition;
layout (location = 1) in vec4 aColor;

out vec4 vColor;

void main() 
{    
    gl_Position = vec4( aPosition.x / viewPortSize.x * 2 - 1, 
                        aPosition.y / viewPortSize.y * 2 - 1,
                        0,
                        1);
    vColor = aColor;
}