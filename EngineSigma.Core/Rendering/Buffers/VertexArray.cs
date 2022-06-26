﻿using OpenTK.Graphics.OpenGL4;

namespace EngineSigma.Core.Rendering;

public class VertexArray : IBuffer
{
    public int Handle { get; }

    private bool _disposed;

    public VertexArray()
    {
        Handle = GL.GenVertexArray();
        Bind();
        
        //Position
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vertex.Size, 0);
        GL.EnableVertexAttribArray(0);
        
        //TexCoords
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, Vertex.Size, 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);
        
        //Color
        GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, Vertex.Size, 5 * sizeof(float));
        GL.EnableVertexAttribArray(2);
    }

    public void Bind()
    {
        GL.BindVertexArray(Handle);
    }

    public void UnBind()
    {
        GL.BindVertexArray(0);
    }

    ~VertexArray()
    {
        Dispose();
    }
    
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        GL.DeleteVertexArray(Handle);
        GC.SuppressFinalize(this);
    }
}