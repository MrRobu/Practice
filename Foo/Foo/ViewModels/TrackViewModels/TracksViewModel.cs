﻿using Caliburn.Micro;
using Dapper;
using Foo.Models;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Foo.ViewModels
{
    public class TracksViewModel : Screen
    {
        private List<Track> _tracks;

        public TracksViewModel()
        {
            Tracks = Track.All();
            FilterTrack = new Track();
        }

        public void LoadTrackCreateView()
        {
            ShellViewModel.Instance?.ActivateItem(new TrackCreateViewModel());
        }

        public void LoadTrackEditView()
        {
            if (SelectedTrack != null)
                ShellViewModel.Instance.ActivateItem(new TrackEditViewModel((int)SelectedTrack.ID));
        }

        public Track FilterTrack { get; set; }

        public void Filter()
        {
            try
            {
                using (var connection = new OdbcConnection(Helper.ConnectionString("postgres_music_serban")))
                {
                    if (FilterTrack.ID != null)
                    {
                        Tracks = new List<Track> { Track.Find((int)FilterTrack.ID) };
                    }

                    string sql = "SELECT * FROM Artist";

                    List<string> filters = new List<string>();

                    DynamicParameters parameters = new DynamicParameters();

                    if (FilterTrack.ID != null)
                    {
                        filters.Add("ID = ?");
                        parameters.Add("ID", FilterTrack.ID);
                    }

                    if (FilterTrack.Title != "")
                    {
                        filters.Add("Title ILIKE ?");
                        parameters.Add("Title", "%" + FilterTrack.Title + "%");
                    }

                    //if(FilterTrack.Genre.Title)

                    //if (FilterTrack.Album.Title != "")
                    //{
                    //    filters.Add(" ILIKE ?");
                    //    parameters.Add("Origin", "%" + FilterBand.Origin + "%");
                    //}

                    if (filters.Count > 0)
                        sql += " WHERE " + string.Join(" AND ", filters);

                    Tracks = connection.Query<Track>(sql, parameters).ToList();
                }
            }
            catch (OdbcException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public List<Track> Tracks
        {
            get
            {
                return _tracks;
            }
            set
            {
                _tracks = value;
                NotifyOfPropertyChange(() => Tracks);
            }
        }

        public Band SelectedTrack { get; set; }
    }
}
