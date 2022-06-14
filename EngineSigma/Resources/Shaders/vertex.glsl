#version 330 core

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aColor;

out vec4 vertexColor;

void main()
{
    vertexColor = vec4(aColor, 1.0);
    gl_Position = vec4(aPosition.xyz, 1.0);
}