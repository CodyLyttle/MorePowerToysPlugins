using System;
using System.Collections.Generic;
using ManagedCommon;
using Wox.Plugin;

namespace Microsoft.PowerToys.Run.Plugin.Google
{
    public class Main : IDisposable, IPlugin, IPluginI18n
    {
        private bool _disposed;
        private PluginInitContext _context;
        
        public string Name => "Google Search";
        
        public string Description => "Search google with Microsoft Edge.\r\n(default browser support coming soon)";
        
        // ReSharper disable once MemberCanBePrivate.Global
        public string IconPath { get; private set; }

        public List<Result> Query(Query query)
        {
            var results = new List<Result>();

            results.Add(CreateResult(query, false));
            results.Add(CreateResult(query, true));
            return results;
        }

        public void Init(PluginInitContext context)
        {
            _context = context ?? throw new ArgumentNullException(paramName: nameof(context));
            _context.API.ThemeChanged += OnThemeChanged;
            UpdateIconPath(_context.API.GetCurrentTheme());
        }

        // TODO: Translate title & description
        public string GetTranslatedPluginTitle()
        {
            return Name;
        }

        public string GetTranslatedPluginDescription()
        {
            return Description;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || !disposing)
                return;

            if (_context?.API != null)
            {
                _context.API.ThemeChanged -= OnThemeChanged;
            }

            _disposed = true;
        }

        protected virtual Result CreateResult(Query query, bool useIncognito)
        {
            string subTitle = useIncognito
                ? "Search Google in a private"
                : "Search Google";
            
            return new Result
            {
                Title = query.Search,
                SubTitle = subTitle,
                IcoPath = IconPath,
                Action = x =>
                {
                    GoogleSearch.OpenInEdge(query.Search, useIncognito);
                    return true;
                }
            };
        }

        private void OnThemeChanged(Theme currentTheme, Theme newTheme)
        {
            UpdateIconPath(newTheme);
        }
        
        private void UpdateIconPath(Theme theme)
        {
            IconPath = theme == Theme.Light 
                ? "Images/GoogleLight.png"
                : "Images/GoogleDark.png";
        }
    }
}