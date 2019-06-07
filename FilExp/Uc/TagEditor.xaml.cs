using MaterialDesignThemes.Wpf;
using Me.Amon.Dao;
using Me.Amon.FilExp.Dto;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace Me.Amon.FilExp.Uc
{
    /// <summary>
    /// TagEditor.xaml 的交互逻辑
    /// </summary>
    public partial class TagEditor : UserControl
    {
        private DocDto _Doc;
        private List<TagDto> _Tags = new List<TagDto>();

        public TagEditor()
        {
            InitializeComponent();
        }

        public void ListTag(DocDto dto)
        {
            _Doc = dto;
            if (_Doc == null)
            {
                return;
            }

            var list = new TagDao().List(dto);
            TagList.Children.Clear();
            foreach (var item in list)
            {
                ShowTag(item);
                _Tags.Add(item);
            }
        }

        private void TxtTag_KeyDown(object sender, KeyEventArgs e)
        {
            AppendTag();
        }

        private void AppendTag()
        {
            var text = TxtTag.Text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            TxtTag.Text = "";

            if (_Doc == null)
            {
                return;
            }

            var tagDao = new TagDao();
            var tagDto = tagDao.Read(text);
            if (tagDto == null)
            {
                tagDto = new TagDto();
                tagDto.names = text;
            }
            tagDto.qty += 1;
            tagDao.Save(tagDto);

            var docTagDao = new DocTagDao();
            var docTagDto = docTagDao.Read(_Doc.id, tagDto.id);
            if (docTagDto != null)
            {
                return;
            }

            docTagDto = new DocTagDto();
            docTagDto.doc_id = _Doc.id;
            docTagDto.tag_id = tagDto.id;
            docTagDao.Save(docTagDto);

            ShowTag(tagDto);
            _Tags.Add(tagDto);
        }

        private void ShowTag(TagDto dto)
        {
            var label = new Chip();
            label.IsDeletable = true;
            label.Content = dto.names;
            label.ToolTip = dto.names;
            label.Tag = dto;
            label.Margin = new System.Windows.Thickness(3);
            label.DeleteClick += Label_DeleteClick;

            TagList.Children.Add(label);
        }

        private void Label_DeleteClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var label = sender as Chip;
            if (label == null)
            {
                return;
            }

            var tagDto = label.Tag as TagDto;
            if (tagDto == null)
            {
                return;
            }

            new DocTagDao().Delete(_Doc.id, tagDto.id);
            new TagDao().UpdateQty(tagDto.id, -1);

            TagList.Children.Remove(label);
            _Tags.Remove(tagDto);
        }
    }
}
