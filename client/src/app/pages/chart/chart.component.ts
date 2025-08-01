import { Component, inject, ChangeDetectionStrategy, OnInit } from "@angular/core";

import { MatDialog } from "@angular/material/dialog";
import { MatFabButton } from "@angular/material/button";
import { MatIcon } from "@angular/material/icon";
import { MatTooltip } from "@angular/material/tooltip";

import { ChartService } from "../../services/chart.service";
import { SettingsComponent } from "../../components/picker/settings.component";

@Component({
  selector: "app-chart",
  templateUrl: "./chart.component.html",
  styleUrls: ["./chart.component.scss"],
  imports: [MatFabButton, MatIcon, MatTooltip],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ChartComponent implements OnInit {
  readonly cht = inject(ChartService);
  private readonly settingsDialog = inject(MatDialog);

  ngOnInit(): void {
    this.cht.loadCharts();
  }

  // SETTINGS DIALOG
  openSettingsDialog(): void {
    this.settingsDialog.open(SettingsComponent, {
      autoFocus: "dialog"
    });
  }
}
