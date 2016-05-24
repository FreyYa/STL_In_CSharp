using QuantumConcepts.Formats.StereoLithography;
using System;
using System.IO;
using System.Windows.Forms;

namespace STL_Csharp
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			STLDocument file;
			FileStream temp = null;

			dlg.Title = "STL 바이너리 파일 열기";
			dlg.Multiselect = false;
			dlg.Filter = "STL 파일(*.stl)|*.stl|모든 파일(*.*)|*.*";

			if (DialogResult.OK != dlg.ShowDialog())
			{
				MessageBox.Show("파일 선택을 해줘!");
				return;
			}


			string path = dlg.FileName;
			if (!File.Exists(path)) return;


			try
			{
				if (File.Exists(dlg.SafeFileName.Replace(".stl", ".txt")))
				{
					if (DialogResult.Yes != MessageBox.Show("이미 txt파일이 존재합니다. 덮어쓰시겠습니까?", "중복 오류", MessageBoxButtons.YesNo))
					{
						MessageBox.Show("프로그램을 종료합니다.");
						return;
					}
				}
				Console.WriteLine("STL 파일을 엽니다");
				if (!STLDocument.IsBinary(temp = new FileStream(dlg.FileName, FileMode.Open)))
				{
					MessageBox.Show("이미 텍스트 파일입니다. 프로그램을 종료합니다");
					return;
				}
				temp.Dispose();

				file = STLDocument.Open(path);
				Console.WriteLine("STL 파일을 ASCII파일로 변환중");
				temp = new FileStream(dlg.SafeFileName.Replace(".stl", ".txt"), FileMode.Create);
				file.WriteText(temp);
				MessageBox.Show(dlg.SafeFileName + " 변환 완료");
			}
			finally
			{
				if (temp != null)
					temp.Dispose();
			}

		}
	}
}
