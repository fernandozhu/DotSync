//
//  ContentView.swift
//  DotSyncIOS
//
//  Created by Fernando Zhu on 1/11/22.
//

import SwiftUI
import CoreData

struct MainView: View {
    @Environment(\.managedObjectContext) private var viewContext

    @FetchRequest(
        sortDescriptors: [NSSortDescriptor(keyPath: \Item.timestamp, ascending: true)],
        animation: .default)
    private var items: FetchedResults<Item>

    var body: some View {
        TabView {
            PhotosView().tabItem {
                Label("Hello", systemImage: "photo.on.rectangle.angled")
            }
            SettingsView().tabItem {
                Label("Settings", systemImage: "list.dash")
            }
        }
    }
}

struct MainView_Previews: PreviewProvider {
    static var previews: some View {
        MainView().environment(\.managedObjectContext, PersistenceController.preview.container.viewContext)
    }
}
