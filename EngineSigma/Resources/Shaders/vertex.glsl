#version 330 core

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoord;
layout (location = 2) in vec4 aColor;

uniform mat4 projection;
uniform mat4 transform;

out vec2 texCoord;
out vec4 vertColor;

void main()
{
    texCoord = aTexCoord;
    vertColor = aColor;
    gl_Position = projection * transform * vec4(aPosition.xyz, 1.0);
}