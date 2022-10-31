//
//  DotSyncIOSApp.swift
//  DotSyncIOS
//
//  Created by Fernando Zhu on 29/10/22.
//

import SwiftUI

@main
struct DotSyncIOSApp: App {
    let persistenceController = PersistenceController.shared

    var body: some Scene {
        WindowGroup {
            ContentView()
                .environment(\.managedObjectContext, persistenceController.container.viewContext)
        }
    }
}
