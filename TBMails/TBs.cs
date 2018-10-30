using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using FastTreeNS;

namespace TBMails
{
    public partial class TBs : Form
    {
        public TBs()
        {
            InitializeComponent();
        }
        private Serializer ser = new Serializer();
        private List<string> ListDirs = new List<string>() { @"\\edp.pt\edpsc\EDPSC_DAC_Area de suporte ao negocio\02. Planeamento & Controlo\03. Reporting\", @"\\edp.pt\edpsc\EDPSC_DRC-CC_Operação Odivelas\Coordenação\Áreas transversais\Planeamento e Controlo\" };
        private TBSettings tbsSettings = new TBSettings();
        private void TBs_Load(object sender, EventArgs e)
        {
            tbsSettings.MainFolder = new List<TBSettings.TB_MainFolder>();
            tbsSettings.MainFolder.Add(new TBSettings.TB_MainFolder() { Nome = "03. Reporting", Path = @"\\edp.pt\edpsc\EDPSC_DAC_Area de suporte ao negocio\02. Planeamento & Controlo\03. Reporting\", Folders = new List<TBSettings.TB_MainFolder.TB_SubFolder>()});
            paths.DataSource = ListDirs;
            RefreshTree();
        }

        private void addPathBt_Click(object sender, EventArgs e)
        {
            paths.Items.Add(pathTXT.Text);
            RefreshTree();
        }
        private void RefreshTree()
        {
            foreach (var caminho in paths.Items){
                TreeNode main = new TreeNode(caminho.ToString());
                List<TBSettings.TB_MainFolder.TB_SubFolder> sub = new List<TBSettings.TB_MainFolder.TB_SubFolder>();
                tbsSettings.MainFolder.Add( new TBSettings.TB_MainFolder() { Nome = caminho.ToString(),   Path= caminho.ToString(), Folders = sub });

                foreach (var file in new DirectoryInfo(caminho.ToString()).GetDirectories("*Reporting*"))
                {
                    TreeNode folder = new TreeNode(file.Name);
                    main.Nodes.Add(searchFoldersAndFiles(caminho.ToString() + "\\" + file.Name, folder));
                }
                TBSTree.Nodes.Add(main);
                PrintNodesRecursive(main, main.Text);
                HideCheckBox(TBSTree, main);
            }
         
        }
        /// <summary>
        /// Corre pelas pastas e esconde a caixa de selecao
        /// </summary>
        /// <param name="oParentNode"></param>
        /// <param name="Level"></param>
        public void PrintNodesRecursive(TreeNode oParentNode, String Level )
        {
            // Start recursion on all subnodes.
            foreach (TreeNode oSubNode in oParentNode.Nodes)
            {
                try
                {                    
                    if (!oSubNode.IsExpanded && !File.Exists(Level + oSubNode.Text)) { HideCheckBox(TBSTree, oSubNode); PrintNodesRecursive(oSubNode, Level + oSubNode.Text+"\\"); }
               } catch  {  }
            }
        }
       /// <summary>
       /// Função para returnar as pastas e subpastas
       /// </summary>
       /// <param name="path">Caminho a procurar</param>
       /// <param name="folder">Tree node para ser adiocionado as subfolders</param>
       /// <returns></returns>
        private TreeNode searchFoldersAndFiles(string path,TreeNode folder)
        {
            foreach (var item in new DirectoryInfo(path).GetDirectories())
            {
                TreeNode sub = new TreeNode(item.Name);
                folder.Nodes.Add(searchFoldersAndFiles(item.FullName, sub));
            }
            foreach (var item in new DirectoryInfo(path).GetFiles())
            {
                if (item.Attributes == FileAttributes.Archive && item.Attributes != FileAttributes.NotContentIndexed && item.Attributes != FileAttributes.Hidden)
                     folder.Nodes.Add(item.Name);
            }
            return folder;
        }
        #region HideCheck
        private const int TVIF_STATE = 0x8;
        private const int TVIS_STATEIMAGEMASK = 0xF000;
        private const int TV_FIRST = 0x1100;
        private const int TVM_SETITEM = TV_FIRST + 63;

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Auto)]
        private struct TVITEM
        {
            public int mask;
            public IntPtr hItem;
            public int state;
            public int stateMask;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszText;
            public int cchTextMax;
            public int iImage;
            public int iSelectedImage;
            public int cChildren;
            public IntPtr lParam;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam,
                                                 ref TVITEM lParam);

        /// <summary>
        /// Hides the checkbox for the specified node on a TreeView control.
        /// </summary>
        private void HideCheckBox(TreeView tvw, TreeNode node)
        {
            TVITEM tvi = new TVITEM();
            tvi.hItem = node.Handle;
            tvi.mask = TVIF_STATE;
            tvi.stateMask = TVIS_STATEIMAGEMASK;
            tvi.state = 0;
            SendMessage(tvw.Handle, TVM_SETITEM, IntPtr.Zero, ref tvi);
        }
        #endregion

        private void TBSTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked)
            {
                GetTBParent(e.Node);
            }
            else
            {
                foreach (var item in tbsSettings.MainFolder)
                {
                    try { GetFile(item.Folders, e.Node.Text).Ativo = e.Node.Checked; } catch (Exception) { }
                }
            }
           
        }

        private string GetTBParent(TreeNode tb, string path = "")
        {
           if(tb.Parent == null)
            {
                return tb.Text + path;
            }
            else
            {
               return GetTBParent(tb.Parent,'\\'+ tb.Text + path);
            }
        }
        
        private TBSettings.TB_MainFolder.TB_File GetFile(List<TBSettings.TB_MainFolder.TB_SubFolder> folders, string file)
        {
            TBSettings.TB_MainFolder.TB_File t = new TBSettings.TB_MainFolder.TB_File();
            foreach (var item in folders)
            {
               t = item.Ficheiros.Find(x => x.Nome == file);
                if (t != null) return t; else return GetFile(item.Folders, file);
            }
           return null;
        }
    }
}

