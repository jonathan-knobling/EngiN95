#version 330 core

out vec4 color;

in vec2 texCoord;
in vec4 vertColor;
uniform sampler2D texture0;

void main()
{
    color = texture(texture0, texCoord) * vertColor;
}