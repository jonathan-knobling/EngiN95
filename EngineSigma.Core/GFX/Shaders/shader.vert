#version 330 core

uniform vec2 viewPortSize;
uniform mat4 transform;

layout (location = 0) in vec2 aPosition;
layout (location = 1) in vec2 aTexCoord;
layout (location = 2) in vec4 aColor;

out vec2 texCoord;
out vec4 vColor;

void main()
{
    texCoord = aTexCoord;
    vColor = aColor;

    gl_Position = vec4(aPosition.x / viewPortSize.x * 2 - 1, aPosition.y / viewPortSize.y * 2 - 1, 0, 1) * transform;
}