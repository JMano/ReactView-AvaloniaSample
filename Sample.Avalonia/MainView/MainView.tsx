import * as React from "react";
import ViewPlugin from "./../ViewPlugin/ViewPlugin";
import { IPluginsContext } from "PluginsProvider";
import "./MainView.scss"; // import a stylesheet

// component properties ... the interface name must start with I prefix and end with Properties suffix
export interface IMainViewProperties {
    contextMenuButtonClicked(): void;
}

export default class MainView extends React.Component<IMainViewProperties> {

    private readonly viewplugin: ViewPlugin;

    constructor(props: IMainViewProperties, context: IPluginsContext) {
        super(props, context);
        this.viewplugin = context.getPluginInstance<ViewPlugin>(ViewPlugin);
    }

    public componentDidMount(): void {
        this.viewplugin.notifyViewLoaded("Main View");
    }

    private onContextMenuButtonClicked = () => {
        this.props.contextMenuButtonClicked();
    };

    public render(): JSX.Element {
        return (
            <div className="wrapper">
                <div className="title">Context Menu isn't navigable</div>
                <button className="task-add" onClick={this.onContextMenuButtonClicked}>Show context menu</button>
            </div>
        );
    }
}