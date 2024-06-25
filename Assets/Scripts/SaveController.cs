using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class SaveController
{
    public void Save()
    {

    }

    public void Load()
    {

    }
}

public class SaveFileIO
{

}

public interface ISaveFileBase
{
    Task Load();
    Task Save();
}

public class SaveFile
{
    public String Path;
    // private byte[] _data = null;
    // private string _strData;
    // private Dictionary<string, object> _dicData;

    // public void GetData(out byte[] data)
    // {
    //     data = _data;
    // }

    // public void GetData(out string data)
    // {
    //     data = _strData;
    // }

    // public void GetData(out Dictionary<string, object> data)
    // {
    //     data = _dicData;
    // }

    // public void SetData(ref Dictionary<string, object> data) => _dicData = data;
    // public void SetData(ref string data) => _strData = data;
    // public void SetData(ref byte[] data) => _data = data;


    private object _data;
    public T GetData<T>()
    {
        return (T)_data;
    }

    public void SetData<T>(T data)
    {
        _data = data;
    }

}

public class SaveFileBase : AbsSaveFileBase
{
    public SaveFileBase(SaveFile file, AbsSaveFileBase saveFile = null) : base(saveFile)
    {
        this.file = file;
        _saveFileBase = saveFile;
    }
    public override Task Load()
    {
        throw new NotImplementedException();
    }

    public override Task Save()
    {
        throw new NotImplementedException();

    }
}

public abstract class AbsSaveFileBase : ISaveFileBase
{
    protected SaveFile file;
    protected ISaveFileBase _saveFileBase;

    public AbsSaveFileBase(AbsSaveFileBase saveFile)
    {
        _saveFileBase = saveFile;
        file = saveFile?.file;
    }

    public abstract Task Load();

    public abstract Task Save();

}

public class EncryAndDecryDecorator : AbsSaveFileBase
{
    public EncryAndDecryDecorator(AbsSaveFileBase file) : base(file)
    {

    }

    public override Task Load()
    {
        throw new NotImplementedException();
    }

    public override Task Save()
    {
        throw new NotImplementedException();
    }
}

public class SerializeAndDeserializeDecorator : AbsSaveFileBase
{
    public SerializeAndDeserializeDecorator(AbsSaveFileBase file) : base(file)
    {
    }

    public override Task Load()
    {
        throw new NotImplementedException();
    }

    public override Task Save()
    {
        throw new NotImplementedException();
    }
}

public class SplitAndMergaDecorator : AbsSaveFileBase
{
    public SplitAndMergaDecorator(AbsSaveFileBase file) : base(file)
    {
    }

    public override Task Load()
    {
        throw new NotImplementedException();
    }

    public override Task Save()
    {
        throw new NotImplementedException();
    }
}

public class AddCRCAndVerifyDecorator : AbsSaveFileBase
{
    public AddCRCAndVerifyDecorator(AbsSaveFileBase file) : base(file)
    {
    }

    public override Task Load()
    {
        throw new NotImplementedException();
    }

    public override Task Save()
    {
        throw new NotImplementedException();
    }
}

public class ReadAndWriteDecorator : AbsSaveFileBase
{
    //增加新测试
    public ReadAndWriteDecorator(AbsSaveFileBase file) : base(file)
    {

    }

    public void Test()
    {

    }

    public override async Task Load()
    {
        if (!System.IO.File.Exists(this.file.Path))
        {
            throw new System.IO.FileNotFoundException(" path: " + file.Path);

        }
        using (FileStream fs = new FileStream(this.file.Path, FileMode.Open, FileAccess.Read))
        {
            int i = 0;
            using (BinaryReader reader = new BinaryReader(fs))
            {

                while (fs.Length > i++)
                {
                    Debug.Log(reader.ReadByte());
                }
            }

        }
    }

    public override async Task Save()
    {
        Debug.Log(file);

        using (FileStream fs = new FileStream(file.Path, FileMode.Create, FileAccess.Write))
        {

            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                writer.Write(file.GetData<byte[]>());
            }
        }
    }
}
