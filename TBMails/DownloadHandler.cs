// Copyright © 2010-2017 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Collections.Generic;
using TBMails;

namespace CefSharp.Example
{
    public class DownloadHandler : IDownloadHandler
    {
        public event EventHandler<DownloadItem> OnBeforeDownloadFired;

        public event EventHandler<DownloadItem> OnDownloadUpdatedFired;

        private string _path;
        public List<Alertas> _lista { get; }

       

        public DownloadHandler(string path,ref List<Alertas> lista)
        {
            //  teste = form;
            _lista = lista;
            _path = path;
        }

    

        public void OnBeforeDownload(IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            var handler = OnBeforeDownloadFired;
            if (handler != null)
            {
                handler(this, downloadItem);
            }

            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    callback.Continue(_path + "\\" + downloadItem.SuggestedFileName, showDialog: false);
                }
            }
        }

        public void OnDownloadUpdated(IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            var handler = OnDownloadUpdatedFired;
            if (handler != null)
            {
                handler(this, downloadItem);
            }
            if( downloadItem.IsComplete) { 
                 _lista.Add(new Alertas() { Alerta = TipoAlerta.Sucesso, Ficheiro = downloadItem.SuggestedFileName, TimeError = DateTime.Now });
            }else if(downloadItem.IsCancelled || !downloadItem.IsValid)
            {
                _lista.Add(new Alertas() { Alerta = TipoAlerta.Erro, Ficheiro = downloadItem.SuggestedFileName, TimeError = DateTime.Now });
            }
        }


    }

}
  