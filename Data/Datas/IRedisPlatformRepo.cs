using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Datas
{
    public interface IRedisPlatformRepo
    {
        #region IEnumerable ve List Farkı
        //List, IEnumrable'dan türer. IEnumerable, daha hızlıdır. Read Onlu Olduğu için Add,Remove yapılmaz, Iteration,sort,filter gibi işlemler yapılır.
        #endregion
        bool CreatePlatform(Platform platform);
        bool CreatePlatformWithHash(Platform platform);
        IEnumerable<Platform?>? GetAllPlatformHash();
        Platform? GetPlatformById(string id);
        IEnumerable<Platform?>? GetAllPlatform();
    }
}
