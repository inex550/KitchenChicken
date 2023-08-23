using System;

public interface IProgressEventsSender {
    event Action<float> OnProgressChanged;
}